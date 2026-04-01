1) create following prompt:
private string _AI_specific_instructions = 
        "Act like a content filter. " +
        "Analyze the following text only for the presence of phrases that clearly refer to:" +
        "1) Diseases (e.g., names of illnesses or medical conditions — but only if it’s clear from context that the word refers to a disease); " +
        "2) Racial information (race, ethnicity, or national origin); " +
        "3) Political preferences (political parties, ideologies, ffiliations. " +
        "If the text contains a phrase clearly matching any of these categories, respond with true. " +
        "Otherwise, respond with false. " +
        "Do not respond with anything else — no explanations, no lists, no formatting. " +
        "Just true or false.";
		
2) call llama3 client from Docker

3) provide a list of examples ('_inputTexts')

4) run console and wait for a text phrase
