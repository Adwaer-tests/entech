using Microsoft.AspNetCore.Mvc.Testing;
using host.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using domain;
using System.Xml.Linq;

namespace host.tests;

[TestFixture]
public class Tests
{
    private HttpClient _client;
    private CustomWebApplicationFactory<Program> _factory;


    [SetUp]
    public void Setup()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });
    }

    [TearDown]
    public void Teardown()
    {
        _factory.Dispose();
        _client.Dispose();
    }


    [Test]
    public async Task Test_Get()
    {
        var animals = await GetAll();
        Assert.IsNotNull(animals);
    }

    [Test]
    [TestCase(null, ExpectedResult = HttpStatusCode.BadRequest)]
    [TestCase("", ExpectedResult = HttpStatusCode.BadRequest)]
    [TestCase("C", ExpectedResult = HttpStatusCode.BadRequest)]
    [TestCase("Ca", ExpectedResult = HttpStatusCode.Created)]
    [TestCase("Cat", ExpectedResult = HttpStatusCode.Created)]
    public async Task<HttpStatusCode> Test_Add(string? name)
    {
        var requestModel = new AnimalCreateRequest() { Name = name };
        var stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/animals", stringContent);
        if (response.IsSuccessStatusCode)
        {
            var animals = await GetAll();
            var animal = animals.FirstOrDefault(a => a.Name == name);
            Assert.IsNotNull(animal);
        }

        return response.StatusCode;
    }

    [Test]
    [TestCase(0, ExpectedResult = HttpStatusCode.BadRequest)]
    [TestCase(-10, ExpectedResult = HttpStatusCode.BadRequest)]
    [TestCase(10, ExpectedResult = HttpStatusCode.NotFound)]
    public async Task<HttpStatusCode> Test_Delete(int id)
    {
        var response = await _client.DeleteAsync($"/animals/{id}");
        if (response.IsSuccessStatusCode)
        {
            var animals = await GetAll();
            var animal = animals.FirstOrDefault(a => a.Id == id);
            Assert.IsNull(animal);
        }

        return response.StatusCode;
    }

    [Test]
    [TestCase("Cat", ExpectedResult = HttpStatusCode.NoContent)]
    public async Task<HttpStatusCode> Test_Add_Delete(string name)
    {
        var requestModel = new AnimalCreateRequest() { Name = name };
        var stringContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("/animals", stringContent);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var animal = JsonConvert.DeserializeObject<AnimalViewModel>(json);

        response = await _client.DeleteAsync($"/animals/{animal.Id}");
        return response.StatusCode;
    }

    private async Task<AnimalViewModel[]> GetAll()
    {
        var response = await _client.GetAsync("/animals");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<AnimalViewModel[]>(json);
    }
}
