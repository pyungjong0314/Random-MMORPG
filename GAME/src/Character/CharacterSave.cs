using System;
using System.IO;
using Game.Characters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1.Characters
{
    public class ValueTupleConverter : JsonConverter<(int x, int y)>
    {
        public override void WriteJson(JsonWriter writer, (int x, int y) value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            writer.WriteEndObject();
        }

        public override (int x, int y) ReadJson(JsonReader reader, Type objectType, (int x, int y) existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            int x = obj["x"].Value<int>();
            int y = obj["y"].Value<int>();
            return (x, y);
        }
    }

        public static class CharacterStorage
    {
        static string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),"character.json");

        public static void SaveCharacter(Character character)
        {
            var settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.Converters.Add(new ValueTupleConverter());

            string json = JsonConvert.SerializeObject(character, settings);
            File.WriteAllText(filePath, json);
        }

        public static Character LoadCharacter()
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("저장된 캐릭터 파일을 찾을 수 없습니다.", filePath);

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new ValueTupleConverter());

            string json = File.ReadAllText(filePath);
            Character character = JsonConvert.DeserializeObject<Character>(json, settings);
            return character;
        }
    }
}