using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TaskFlow.Api.Contracts
{
    public interface IJsonExtensionDataContainer
    {
        [JsonExtensionData]
        Dictionary<string, JsonElement>? ExtensionData { get; set; }
    }
}
