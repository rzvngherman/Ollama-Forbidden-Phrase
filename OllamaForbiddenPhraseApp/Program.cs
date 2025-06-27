using Newtonsoft.Json;
using System.Net.Http.Headers;

internal class Program
{
    //https://github.com/ollama/ollama/blob/main/docs/api.md
    // "AI response is 'non-deterministic' means that identical inputs can produce different outputs when run multiple times."

    private const string _AI_URL = "http://host.docker.internal:11434/api/chat";
    private const string _AI_specific_instructions = "Act like a content filter. Analyze the following text only for the presence of phrases that clearly refer to:1) Diseases (e.g., names of illnesses or medical conditions — but only if it’s clear from context that the word refers to a disease); 2) Racial information (race, ethnicity, or national origin); 3) Political preferences (political parties, ideologies, ffiliations. If the text contains a phrase clearly matching any of these categories, respond with true. Otherwise, respond with false. Do not respond with anything else — no explanations, no lists, no formatting. Just true or false.";

    private HttpClient _httpClient;

    public Program(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private static async Task Main(string[] args)
    {
        var p = new Program(new HttpClient());
        //await p.Do();
        await p.Do2();
    }
    
    private async Task Do()
    {
        foreach (var text in _inputTexts)
        {
            Console.WriteLine("'" + text + "'");
            await CheckTextForForbidden(text);
            Console.WriteLine("");
        }

        Console.ReadLine();
    }

    private async Task Do2()
    {
        Console.WriteLine("!!! AI response is 'non-deterministic' means that identical inputs can produce different outputs when run multiple times.");

        Console.WriteLine("");
        Console.WriteLine("Analyze text and return 'true' if in one of the following category:");
        Console.WriteLine("1) Diseases (e.g., names of illnesses or medical conditions — but only if it’s clear from context that the word refers to a disease)");
        Console.WriteLine("2) Racial information (race, ethnicity, or national origin),");
        Console.WriteLine("3) Political preferences (political parties, ideologies, affiliations)");

        Examples();

        ConsoleKeyInfo key1 = new ConsoleKeyInfo((char)ConsoleKey.D1, ConsoleKey.D1, false, false, false);
        do
        {
            Console.WriteLine("");
            Console.Write(">>> ");
            var text = Console.ReadLine();

            await CheckTextForForbidden(text);

            Console.WriteLine("");
        }
        while (key1.Key == ConsoleKey.D1);

        Console.WriteLine("-- END --");
        Console.ReadLine();
    }

    private void Examples()
    {
        Console.WriteLine("");
        Console.WriteLine("->Input examples:");
        foreach (var item in _inputTexts)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("");
    }

    private async Task CheckTextForForbidden(string inputText)
    {
        Console.WriteLine("--AI is thinking..." + "(" + DateTime.Now + ")");

        var content = _requestDataTemplate
                        .Replace("#REQUEST_DATA#", inputText)
                        .Replace("#AI_specific_instructions#", _AI_specific_instructions);

        StringContent queryString = new StringContent(content);
        HttpResponseMessage httpResponseMessage = await _httpClient.PostAsync(new Uri(_AI_URL), queryString);
        httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        httpResponseMessage.EnsureSuccessStatusCode();
        string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
        ResponseModel responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseBody);

        Console.WriteLine(responseModel.message.content);
    }

    private string _requestDataTemplate =
        @"
        {
            ""model"": ""llama3"",
            ""messages"": [
                {
                    ""role"": ""system"",
                    ""content"": ""#AI_specific_instructions#""
                },
                {
                    ""role"": ""user"",
                    ""content"": ""#REQUEST_DATA#""
                }
            ],
            ""stream"": false
        }
        ";

    private ResponseModel m = new ResponseModel();

    private string[] _inputTexts = new string[]
    {
        // !!! AI responses being 'non-deterministic' means that identical inputs can produce different outputs when run multiple times. 



        //TRUE (false positive)
        "They study human heart in medical schools.",

        //TRUE
        "He has heart disease.",

        //TRUE (???)
        "cancer",

        //TRUE(false positive)
        "Cancer is a medical condition.",

        //FALSE
        "Cancer is the fourth sign of the zodiac.",

        //TRUE
        "He told me that a person near to him has cancer.",

        //FALSE
        "F**k bitch !",

        //TRUE
        "He is black."

        ,"he's skin is black",

        //FALSE
        "Room is black when is sunny",

        //FALSE
        "Room is black when is dark"

        //TRUE
        , "They like 'XYZ' political party"

        //FALSE
        ,"Men in black."
    };

    public class ResponseModel
    {
        public ResponseModelMessageTag message { get; set; }
    }

    public class ResponseModelMessageTag
    {
        public string content { get; set; }
    }
}