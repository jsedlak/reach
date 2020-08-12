using Newtonsoft.Json;
using System;

namespace Reach.DataModel
{
    public abstract class Entity
    {
        /// <summary>
        /// Gets or Sets the unique identifier for this entity
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
