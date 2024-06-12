﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace AppointmentBooking.Api.Converters
{
    public class JsonDateTimeConverterWithFormat : JsonConverter<DateTime>
    {
        private readonly string _format;

        public JsonDateTimeConverterWithFormat(string format)
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
