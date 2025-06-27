# Ollama-Forbidden-Phrase

1) Create an AI-model trained for forbidden phrases that can contain:
- diseases, 
- racial information
- and political preferences.

2) use offline AI

3) use 'llama3' from ollama

4) AI prompt text:
Act like a content filter. 
Analyze the following text only for the presence of phrases that clearly refer to:
a) Diseases (e.g., names of illnesses or medical conditions — but only if it’s clear from context that the word refers to a disease); 
b) Racial information (race, ethnicity, or national origin); 
c) Political preferences (political parties, ideologies, ffiliations. 
If the text contains a phrase clearly matching any of these categories, respond with true. 
Otherwise, respond with false. 
Do not respond with anything else — no explanations, no lists, no formatting. Just true or false.


5) use Docker Desktop

6) commands:
a) docker pull ollama/ollama
(1.71 gb)

b) docker run -d --gpus=all -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama
(optional, with out gpus=all: 'docker run -d -v ollama:/root/.ollama -p 11434:11434 --name ollama ollama/ollama')

c) docker exec -it ollama ollama run llama3


7) don't forget:
!!! AI response is 'non-deterministic' means that identical inputs can produce different outputs when run multiple times.

8) urls
a) https://github.com/ollama/ollama/blob/main/docs/api.md
b) https://github.com/imantsm/medical_abbreviations/tree/master/CSVs
