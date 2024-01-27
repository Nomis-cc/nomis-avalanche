// ------------------------------------------------------------------------------------------------------
// <copyright file="NFTMetadata.cs" company="Nomis">
// Copyright (c) Nomis, 2023. All rights reserved.
// The Application under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>
// ------------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Nomis.Utils.Contracts.NFT
{
    /// <summary>
    /// NFT metadata.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class NFTMetadata
    {
        /// <summary>
        /// A human readable description of the item.
        /// </summary>
        /// <remarks>
        /// Markdown is supported.
        /// </remarks>
        [JsonPropertyName("description")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Description { get; set; }

        /// <summary>
        /// This is the URL that will appear below the asset's image on OpenSea and will allow users to leave OpenSea and view the item on your site.
        /// </summary>
        [JsonPropertyName("external_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ExternalUrl { get; set; }

        /// <summary>
        /// This is the URL to the image of the item.
        /// </summary>
        /// <remarks>
        /// Can be just about any type of image (including SVGs, which will be cached into PNGs by OpenSea), and can be IPFS URLs or paths. We recommend using a 350 x 350 image.
        /// </remarks>
        [JsonPropertyName("image")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Image { get; set; }

        /// <summary>
        /// Name of the item.
        /// </summary>
        [JsonPropertyName("name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }

        /// <summary>
        /// A URL to a multi-media attachment for the item. The file extensions GLTF, GLB, WEBM, MP4, M4V, OGV, and OGG are supported, along with the audio-only extensions MP3, WAV, and OGA.
        /// </summary>
        /// <remarks>
        /// Animation_url also supports HTML pages, allowing you to build rich experiences and interactive NFTs using JavaScript canvas, WebGL, and more.
        /// Scripts and relative paths within the HTML page are now supported.
        /// However, access to browser extensions is not supported.
        /// </remarks>
        [JsonPropertyName("animation_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AnimationUrl { get; set; }

        /// <summary>
        /// A URL to a YouTube video.
        /// </summary>
        [JsonPropertyName("youtube_url")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? YoutubeUrl { get; set; }

        /// <summary>
        /// These are the attributes for the item, which will show up on the OpenSea page for the item.
        /// </summary>
        [JsonPropertyName("attributes")]
        public IList<NFTTrait> Attributes { get; set; } = new List<NFTTrait>();
    }
}