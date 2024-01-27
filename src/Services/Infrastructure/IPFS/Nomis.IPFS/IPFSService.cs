// ------------------------------------------------------------------------------------------------------
// <copyright file="IPFSService.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Net.Http.Headers;
using System.Net.Http.Json;

using Ipfs.Http;
using Microsoft.Extensions.Options;
using Nomis.IPFS.Enums;
using Nomis.IPFS.Interfaces;
using Nomis.IPFS.Interfaces.Requests;
using Nomis.IPFS.Interfaces.Settings;
using Nomis.IPFS.Responses;
using Nomis.IPFS.Settings;
using Nomis.Utils.Contracts.Services;
using Nomis.Utils.Wrapper;

// ReSharper disable InconsistentNaming
namespace Nomis.IPFS
{
    /// <inheritdoc cref="IIPFSService"/>
    public class IPFSService :
        IIPFSService,
        ISingletonService,
        IDisposable
    {
        private readonly IPFSSettings _ipfsSettings;
        private readonly HttpClient? _web3StorageClient;
        private readonly HttpClient? _pinataClient;
        private readonly HttpClient? _pinataReadClient;

        /// <summary>
        /// Initialize <see cref="IPFSService"/>.
        /// </summary>
        /// <param name="ipfsSettings"><see cref="IPFSSettings"/>.</param>
        public IPFSService(
            IOptions<IPFSSettings> ipfsSettings)
        {
            _ipfsSettings = ipfsSettings.Value;
            Settings = ipfsSettings.Value;
            switch (ipfsSettings.Value.Provider)
            {
                case IPFSProvider.LocalNode:
                    IPFSClient = new(ipfsSettings.Value.ApiBaseUrl ?? "https://api.web3.storage/");
                    break;
                case IPFSProvider.Web3Storage:
                    _web3StorageClient = new HttpClient
                    {
                        BaseAddress = new Uri(ipfsSettings.Value.ApiBaseUrl ?? "https://api.web3.storage/")
                    };
                    _web3StorageClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ipfsSettings.Value.ApiKey ?? throw new ArgumentNullException(nameof(ipfsSettings.Value.ApiKey))}");
                    break;
                case IPFSProvider.Pinata:
                    _pinataClient = new HttpClient
                    {
                        BaseAddress = new Uri(ipfsSettings.Value.ApiBaseUrl ?? "https://api.pinata.cloud/")
                    };
                    _pinataClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ipfsSettings.Value.ApiKey ?? throw new ArgumentNullException(nameof(ipfsSettings.Value.ApiKey))}");
                    _pinataClient.DefaultRequestHeaders.Add("Host", "api.pinata.cloud");

                    _pinataReadClient = new HttpClient
                    {
                        BaseAddress = new Uri(ipfsSettings.Value.ReadApiBaseUrl ?? "https://gold-neat-felidae-598.mypinata.cloud/")
                    };
                    break;
            }
        }

        /// <inheritdoc />
        public IIPFSSettings Settings { get; }

        /// <inheritdoc/>
        public IpfsClient? IPFSClient { get; }

        /// <inheritdoc/>
        public async Task<Result<byte[]>> DownloadFileAsync(
            string fileId)
        {
            switch (_ipfsSettings.Provider)
            {
                case IPFSProvider.LocalNode:
                    if (IPFSClient != null)
                    {
                        var fileStream = await IPFSClient.PostDownloadAsync("cat", default, fileId).ConfigureAwait(false);
                        var ms = new MemoryStream();
                        await using (ms.ConfigureAwait(false))
                        {
                            await fileStream.CopyToAsync(ms).ConfigureAwait(false);
                            byte[] fileBytes = ms.ToArray();
                            return await Result<byte[]>.SuccessAsync(fileBytes, "File downloaded from IPFS (Local node).").ConfigureAwait(false);
                        }
                    }

                    return await Result<byte[]>.FailAsync("IPFS client has not been configured.").ConfigureAwait(false);
                case IPFSProvider.Web3Storage:
                    if (_web3StorageClient != null)
                    {
                        var result = await _web3StorageClient.GetAsync($"/car/{fileId}").ConfigureAwait(false);
                        result.EnsureSuccessStatusCode();

                        if (result.IsSuccessStatusCode)
                        {
                            byte[] fileBytes = await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                            return await Result<byte[]>.SuccessAsync(fileBytes, "File downloaded from IPFS (Web3 Storage).").ConfigureAwait(false);
                        }
                    }

                    return await Result<byte[]>.FailAsync("IPFS Web3 Storage client has not been configured.").ConfigureAwait(false);
                case IPFSProvider.Pinata:
                    if (_pinataReadClient != null)
                    {
                        var result = await _pinataReadClient.GetAsync($"/ipfs/{fileId}").ConfigureAwait(false);
                        result.EnsureSuccessStatusCode();

                        if (result.IsSuccessStatusCode)
                        {
                            byte[] fileBytes = await result.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                            return await Result<byte[]>.SuccessAsync(fileBytes, "File downloaded from IPFS (Pinata).").ConfigureAwait(false);
                        }
                    }

                    return await Result<byte[]>.FailAsync("IPFS Pinata client has not been configured.").ConfigureAwait(false);
            }

            return await Result<byte[]>.FailAsync("IPFS client has not been configured.").ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Result<string?>> UploadFileAsync(
            IPFSUploadFileRequest request)
        {
            switch (_ipfsSettings.Provider)
            {
                case IPFSProvider.LocalNode:
                    if (IPFSClient != null)
                    {
                        var result = await IPFSClient.FileSystem.AddAsync(request.FileContent, request.FileName, request.Options).ConfigureAwait(false);
                        return await Result<string?>.SuccessAsync(result.Id.Encode(), "File uploaded to IPFS (Local node).").ConfigureAwait(false);
                    }

                    return await Result<string?>.FailAsync("IPFS client has not been configured.").ConfigureAwait(false);
                case IPFSProvider.Web3Storage:
                    if (_web3StorageClient != null && request.FileContent != null)
                    {
                        using var ms = new MemoryStream();
                        await request.FileContent.CopyToAsync(ms).ConfigureAwait(false);
                        byte[] fileContent = ms.ToArray();
                        var response = await _web3StorageClient.PostAsync("upload", new ByteArrayContent(fileContent)).ConfigureAwait(false);
                        response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            var result = await response.Content.ReadFromJsonAsync<Web3StorageUploadResponse>().ConfigureAwait(false);
                            return await Result<string?>.SuccessAsync(result?.Cid, "File uploaded to IPFS (Web3 Storage).").ConfigureAwait(false);
                        }
                    }

                    return await Result<string?>.FailAsync("IPFS Web3 Storage client has not been configured.").ConfigureAwait(false);

                case IPFSProvider.Pinata:
                    if (_pinataClient != null && request.FileContent != null)
                    {
                        try
                        {
                            using var content = new MultipartFormDataContent
                            {
                                {
                                    new StreamContent(request.FileContent)
                                    {
                                        Headers =
                                        {
                                            ContentLength = request.FileContent.Length,
                                            ContentType = new MediaTypeHeaderValue("image/png")
                                        }
                                    }, "file", request.FileName ?? "score.png"
                                }
                            };
                            var response = await _pinataClient.PostAsync("pinning/pinFileToIPFS", content).ConfigureAwait(false);
                            response.EnsureSuccessStatusCode();

                            if (response.IsSuccessStatusCode)
                            {
                                var result = await response.Content.ReadFromJsonAsync<PinataUploadResponse>().ConfigureAwait(false);
                                return await Result<string?>.SuccessAsync(result?.IpfsHash, "File uploaded to IPFS (Pinata).").ConfigureAwait(false);
                            }
                        }
                        catch (Exception e)
                        {
                            return await Result<string?>.FailAsync("IPFS client has not been configured (Pinata).", e.Message).ConfigureAwait(false);
                        }

                        return await Result<string?>.FailAsync("IPFS client has not been configured (Pinata).").ConfigureAwait(false);
                    }

                    return await Result<string?>.FailAsync("IPFS Pinata client has not been configured.").ConfigureAwait(false);
            }

            return await Result<string?>.FailAsync("IPFS client has not been configured (Pinata).").ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            _web3StorageClient?.Dispose();
            _pinataClient?.Dispose();
            _pinataReadClient?.Dispose();
        }
    }
}