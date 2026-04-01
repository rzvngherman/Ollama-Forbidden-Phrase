a) create following prompt:

private string _AI_specific_instructions = 
<p>        

        "Act like a content filter. " +
        "Analyze the following text only for the presence of phrases that clearly refer to:" +

        "1) Diseases (e.g., names of illnesses or medical conditions — but only if it’s clear from context that the word refers to a disease); " +

        "2) Racial information (race, ethnicity, or national origin); " +

        "3) Political preferences (political parties, ideologies, ffiliations. " +

        "If the text contains a phrase clearly matching any of these categories, respond with true. " +

        "Otherwise, respond with false. " +

        "Do not respond with anything else — no explanations, no lists, no formatting. " +

        "Just true or false.";
</p>

b) call llama3 client from Docker

c) provide a list of examples ('_inputTexts')

<p>

        //TRUE (false positive)
        "They study human heart in medical schools.",

        //TRUE
        "He has heart disease.",

        //TRUE (depends on context)
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

        //TRUE
        ,"My skin is black",

        //FALSE
        "Room is black when is sunny",

        //FALSE
        "Room is black when is dark"

        //TRUE
        , "They like 'XYZ' political party"

        //FALSE
        ,"Men in black."
</p>

d) console run and wait for an input text phrase
