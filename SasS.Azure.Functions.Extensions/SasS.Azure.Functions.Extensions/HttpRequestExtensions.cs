using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SasS.Azure.Functions.Extensions
{
    /// <summary>
    /// Http Request Extensions
    /// </summary>
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Read Stream Body As String
        /// </summary>
        /// <param name="httpRequest">Http Request</param>
        /// <returns>Stream Body in String format</returns>
        public static async Task<string> ReadBodyAsStringAsync(this HttpRequest httpRequest)
        {
            using (var stream = new StreamReader(httpRequest.Body))
            {
                return await stream.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Read & Parse Json Body into a strongly type class model
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="httpRequest">Http Request</param>
        /// <returns>Deserialized Body in T format</returns>
        public static async Task<T> ReadJsonBodyAsync<T>(this HttpRequest httpRequest)
            where T : class
        {
            var rawBody = await ReadBodyAsStringAsync(httpRequest);
            return JsonConvert.DeserializeObject<T>(rawBody);
        }

        /// <summary>
        /// Read & Parse Form Body into a strongly type class model
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="httpRequest">Http Request</param>
        /// <returns>Deserialized Form Body in T format</returns>
        public static async Task<T> ReadFormBodyAsync<T>(this HttpRequest httpRequest)
            where T : class
        {
            var formCollection = await httpRequest.ReadFormAsync();
            var properties = typeof(T).GetProperties();
            var formBody = Activator.CreateInstance<T>();

            foreach (var property in properties)
            {
                if (formCollection.ContainsKey(property.Name))
                {
                    if (property.PropertyType == typeof(DateTime))
                        property.SetValue(formBody, Convert.ToDateTime(formCollection[property.Name]));
                    else if (property.PropertyType == typeof(bool))
                        property.SetValue(formBody, Convert.ToBoolean(formCollection[property.Name]));
                    else if (property.PropertyType == typeof(decimal))
                        property.SetValue(formBody, Convert.ToDecimal(formCollection[property.Name]));
                    else if (property.PropertyType == typeof(int))
                        property.SetValue(formBody, Convert.ToInt32(formCollection[property.Name]));
                    else
                        property.SetValue(formBody, formCollection[property.Name].ToString());
                }
            }

            return formBody;
        }
    }
}
