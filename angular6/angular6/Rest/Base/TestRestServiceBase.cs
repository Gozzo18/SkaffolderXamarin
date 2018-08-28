using Newtonsoft.Json;
using angular6.Models;
using angular6.Support;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace angular6.Rest.Base
{
    public class TestRestServiceBase : RestClient
    {
        private const string TestApi ="tests/";
        public ObservableCollection<Test> _testlist { get; private set; }

        //DELETE
        /// <summary>
        /// Delete a Test
        /// </summary>
        /// <param name="id">Id of the Test to Delete</param>
        /// <returns>void</returns>
        public async Task DELETE(string id)
        {
            try
            {
                var response = await client.DeleteAsync(TestApi + id);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //POST
        /// <summary>
        /// Add a new Test
        /// </summary>
        /// <param name="item">Test to Add</param>
        /// <returns>void</returns>
        public async Task POST(Test item)
        {
            try
            {
                
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(TestApi, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //PUT
        /// <summary>
        /// Update info of a Test
        /// </summary>
        /// <param name="item">Test to Update</param>
        /// <returns></returns>
        public async Task PUT(Test item)
        {
            try
            {
                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(TestApi + item.Id, content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR{0}", e);
            }
        }

        //GET
        /// <summary>
        /// Get the complete list of Tests
        /// </summary>
        /// <returns>Test List</returns>
        public async Task<ObservableCollection<Test>> GETList()
        {
            _testlist = new ObservableCollection<Test>();
            try
            {
                var content = await client.GetStringAsync(TestApi);
                _testlist = JsonConvert.DeserializeObject<ObservableCollection<Test>>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
                //Send a notify of token expiration, to whoever is subscribed to this RestService
                MessagingCenter.Send<TestRestServiceBase, bool>(this, Events.TokenExpired, true);
            }
            return _testlist;
        }

        //GET ID
        /// <summary>
        /// Get a Test
        /// </summary>
        /// <returns>Test</returns>
        public async Task<Test> GETId(string testId)
        {
            Test test = new Test();
            try
            {
                var content = await client.GetStringAsync(TestApi + testId);
                test = JsonConvert.DeserializeObject<Test>(content);
            }catch (Exception e){
                Debug.WriteLine(@"				ERROR {0}", e);
            }
            return test;
        }
    }
}
