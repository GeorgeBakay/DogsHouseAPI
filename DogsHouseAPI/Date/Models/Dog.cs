using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace DogsHouseAPI.Models
{
    public class EmptyStringConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.String && string.IsNullOrEmpty(token.ToString()))
            {
                return null;
            }
            return token.ToObject(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
    public class Dog
    {

        [Key]
        [JsonPropertyName("name")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; } = "";
    
        [JsonPropertyName("color")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(EmptyStringConverter))]
        public string Color { get; set; } = "";
        [Required]
        [JsonPropertyName("tail_length")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Tail_length { get; set; } = 0;
        [Required]
        [JsonPropertyName("weight")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Weight { get; set; } = 0;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Dog otherDog = (Dog)obj;
            return  Name == otherDog.Name && Weight == otherDog.Weight && Tail_length == otherDog.Tail_length && Color == otherDog.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name,Color,Tail_length,Weight);
        }
    }
}
