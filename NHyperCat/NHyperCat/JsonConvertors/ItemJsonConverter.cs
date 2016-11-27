﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

/* LICENCE INFORMATION 
* Copyright (c) 2016 Ranjan Dailata.
*  
* Enables easy creation and parsing of valid Hypercat catalogues
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

namespace NHyperCat.JsonConvertors
{
    public class ItemJsonConverter : JsonConverter
    {
        public ItemJsonConverter()
        {
            Formatting = Formatting.None;
        }

        public Formatting Formatting { get; set; }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ItemJsonConverter);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var collection = new List<Item>();
            var itemsCollection = new ItemCollection();

            serializer.Populate(reader, collection);
            itemsCollection.AddRange(collection);

            return itemsCollection;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.Formatting = Formatting;
            var items = value as ItemCollection;

            if (items != null && items.Count > 0)
            {
                var itemsStringBuilder = new StringBuilder();

                foreach (var item in items)
                {
                    itemsStringBuilder.Append(
                        $"{JsonConvert.SerializeObject(item)}{","}");
                }

                var json = itemsStringBuilder.ToString().Substring(0,
                    itemsStringBuilder.Length - 1);

                writer.WriteRaw(json);
            }
        }
    }
}
