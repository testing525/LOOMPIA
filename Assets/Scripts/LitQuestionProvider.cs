using UnityEngine;
using System.Collections.Generic;

public static class LitQuestionProvider
{
    //1st str question
    //2nd string answer
    //3rd string multiple choices(include the correct answer, the other 2 random but related)
    private static List<(string, string, string[])> easyQuestions = new List<(string, string, string[])>
    {
        ("She ___ to school every day.", "goes", new string[] { "go", "goes", "going" }),
        ("They ___ playing outside.", "are", new string[] { "is", "are", "am" }),
        ("He ___ a book yesterday.","read", new string[] { "read", "reads", "reading" }),
        ("I ___ happy today", "am", new string[] { "am", "is", "are" }),
        ("We ___ to the park last Sunday.", "went", new string[] { "go", "going", "went" }),
        ("The cat ___ under the table.", "is", new string[] { "is", "are", "am" }),
        ("She ___ an apple now.", "is eating", new string[] { "eats", "is eating", "eat" }),
        ("They ___ football every weekend.", "play", new string[] { "play", "plays", "playing" }),
        ("He ___ tall and strong.", "is", new string[] { "is", "are", "am" }),
        ("The sun ___ in the east.", "rises", new string[] { "rise", "rises", "rising" }),
        ("She ___ her homework every evening.", "does", new string[] { "do", "does", "doing" }),
        ("I ___ my best friend yesterday.", "saw", new string[] { "see", "saw", "seen" }),
        ("The dog ___ loudly last night.", "barked", new string[] { "bark", "barking", "barked" }),
        ("They ___ happy with their new toy.", "are", new string[] { "is", "are", "am" }),
        ("He ___ very fast.", "runs", new string[] { "run", "runs", "running" }),
        ("We ___ to the zoo tomorrow.", "will go", new string[] { "go", "goes", "will go" }),
        ("She ___ tea every morning.", "drinks", new string[] { "drink", "drinks", "drinking" }),
        ("The birds ___ in the sky.", "fly", new string[] { "flies", "fly", "flying" }),
        ("My mother ___ dinner yesterday.", "cooked", new string[] { "cook", "cooking", "cooked" }),
        ("The boy ___ in the pool.", "is swimming", new string[] { "swim", "swims", "is swimming" }),
        ("He ___ his bicycle to school.", "rides", new string[] { "ride", "rides", "riding" }),
        ("I ___ a letter to my friend yesterday.", "wrote", new string[] { "write", "wrote", "writing" }),
        ("She ___ very kind.", "is", new string[] { "is", "are", "am" }),
        ("We ___ our grandparents last week.", "visited", new string[] { "visit", "visiting", "visited" }),
        ("The baby ___ last night.", "cried", new string[] { "cry", "cries", "cried" }),
        ("They ___ books in the library.", "read", new string[] { "reads", "read", "reading" }),
        ("He ___ a new shirt today.", "is wearing", new string[] { "wear", "wears", "is wearing" }),
        ("She ___ to music every day.", "listens", new string[] { "listen", "listens", "listening" }),
        ("We ___ happy yesterday.", "were", new string[] { "was", "were", "are" }),
        ("The dog ___ the ball.", "caught", new string[] { "catch", "catches", "caught" }),
        ("He ___ a story to us.", "tells", new string[] { "tell", "tells", "telling" }),
        ("They ___ dancing at the party.", "were", new string[] { "was", "were", "are" }),
        ("I ___ my keys this morning.", "lost", new string[] { "lose", "lost", "losing" }),
        ("She ___ at the top of her class.", "is", new string[] { "is", "are", "am" }),
        ("We ___ for the test yesterday.", "studied", new string[] { "study", "studied", "studying" }),
        ("He ___ up early every day.", "gets", new string[] { "get", "gets", "getting" }),
        ("They ___ their friends at the park.", "met", new string[] { "meet", "meets", "met" }),
        ("She ___ the guitar very well.", "plays", new string[] { "play", "plays", "playing" }),
        ("I ___ my homework last night.", "finished", new string[] { "finish", "finished", "finishing" }),
        ("He ___ his hands before eating.", "washes", new string[] { "wash", "washes", "washing" }),
        ("We ___ ice cream yesterday.", "ate", new string[] { "eat", "ate", "eaten" }),
        ("The children ___ happy today.", "are", new string[] { "is", "are", "am" }),
        ("She ___ to school by bus.", "goes", new string[] { "go", "goes", "going" }),
        ("They ___ singing loudly.", "are", new string[] { "is", "are", "am" }),
        ("I ___ to bed early last night.", "went", new string[] { "go", "goes", "went" }),
        ("He ___ very fast yesterday.", "ran", new string[] { "run", "ran", "running" }),
        ("The teacher ___ a story yesterday.", "told", new string[] { "tell", "told", "telling" }),
        ("We ___ a movie last night.", "watched", new string[] { "watch", "watched", "watching" }),
        ("She ___ her shoes before entering the room.", "took off", new string[] { "take off", "took off", "taking off" }),
        ("They ___ breakfast early in the morning.", "had", new string[] { "have", "had", "having" })
    };

    private static List<(string, string, string[])> medQuestions = new List<(string, string, string[])>
    {
        ("If it ___ tomorrow, we will stay at home.", "rains", new string[] { "rain", "rains", "raining" }),
        ("She ___ already finished her homework before dinner.", "had", new string[] { "has", "have", "had" }),
        ("They ___ to London last summer.", "traveled", new string[] { "travel", "traveled", "travelling" }),
        ("The book was so interesting that I ___ it twice.", "read", new string[] { "read", "reads", "reading" }),
        ("He ___ the guitar since he was ten.", "has played", new string[] { "plays", "played", "has played" }),
        ("We were tired because we ___ all day.", "had worked", new string[] { "worked", "have worked", "had worked" }),
        ("The movie was ___ than I expected.", "better", new string[] { "good", "better", "best" }),
        ("She sings ___ than her sister.", "more beautifully", new string[] { "beautiful", "more beautifully", "most beautifully" }),
        ("I was reading a book when the phone ___", "rang", new string[] { "rings", "rang", "ringing" }),
        ("By the time he arrived, we ___ dinner.", "had finished", new string[] { "finish", "finished", "had finished" }),
        ("This is the ___ painting in the museum.", "most famous", new string[] { "famous", "more famous", "most famous" }),
        ("If I ___ enough money, I would buy a new car.", "had", new string[] { "have", "had", "having" }),
        ("They ___ each other since kindergarten.", "have known", new string[] { "know", "knew", "have known" }),
        ("The letter ___ yesterday.", "was sent", new string[] { "sent", "was sent", "sending" }),
        ("She ___ her car while I was cooking.", "washed", new string[] { "washes", "washed", "washing" }),
        ("He asked me if I ___ the movie.", "had seen", new string[] { "see", "saw", "had seen" }),
        ("We ___ when the lights went out.", "were studying", new string[] { "study", "studied", "were studying" }),
        ("The teacher told us to ___ quietly.", "listen", new string[] { "listen", "listens", "listening" }),
        ("If I study hard, I ___ pass the exam.", "will", new string[] { "will", "would", "shall" }),
        ("This road is ___ than the other one.", "wider", new string[] { "wide", "wider", "widest" }),
        ("They ___ at this school for three years.", "have studied", new string[] { "studied", "have studied", "studying" }),
        ("He ___ a new phone last week.", "bought", new string[] { "buys", "buy", "bought" }),
        ("The cake ___ by my mother yesterday.", "was baked", new string[] { "bakes", "baked", "was baked" }),
        ("I ___ to the library every Saturday.", "go", new string[] { "go", "goes", "going" }),
        ("When I was young, I ___ to climb trees.", "used", new string[] { "use", "used", "using" }),
        ("She ___ when I entered the room.", "was sleeping", new string[] { "slept", "was sleeping", "sleep" }),
        ("He ___ English for five years.", "has studied", new string[] { "studies", "studied", "has studied" }),
        ("We ___ the project by next week.", "will finish", new string[] { "finish", "finished", "will finish" }),
        ("The boy ___ the ball into the basket.", "threw", new string[] { "throw", "throws", "threw" }),
        ("She ___ very nervous before the test.", "felt", new string[] { "feels", "felt", "feeling" }),
        ("He said he ___ the answer.", "knew", new string[] { "know", "knew", "knows" }),
        ("The flowers ___ in the garden.", "are growing", new string[] { "grow", "grows", "are growing" }),
        ("If I ___ you, I would say sorry.", "were", new string[] { "was", "were", "be" }),
        ("The story ___ by my grandmother.", "was told", new string[] { "told", "was told", "telling" }),
        ("She ___ to the store when I called her.", "was going", new string[] { "went", "was going", "goes" }),
        ("We ___ each other yesterday at the park.", "met", new string[] { "meet", "meets", "met" }),
        ("He ___ in this company since 2010.", "has worked", new string[] { "works", "worked", "has worked" }),
        ("The mountain is the ___ place I’ve ever visited.", "highest", new string[] { "high", "higher", "highest" }),
        ("She ___ her keys in the car yesterday.", "left", new string[] { "leave", "left", "leaves" }),
        ("They ___ to finish the homework before the deadline.", "managed", new string[] { "manage", "managed", "managing" }),
        ("He ___ the news on TV last night.", "watched", new string[] { "watch", "watched", "watching" }),
        ("The students ___ by the teacher.", "were guided", new string[] { "guided", "were guided", "guiding" }),
        ("If it ___, the ground will be wet.", "rains", new string[] { "rain", "rains", "raining" }),
        ("I ___ my phone yesterday, but later I found it.", "lost", new string[] { "lose", "lost", "losing" }),
        ("They ___ their homework before going outside.", "finished", new string[] { "finish", "finishes", "finished" }),
        ("She ___ her bike when it started to rain.", "was riding", new string[] { "rides", "rode", "was riding" }),
        ("We ___ for two hours before the teacher arrived.", "had waited", new string[] { "wait", "waited", "had waited" }),
        ("The concert ___ by many people.", "was attended", new string[] { "attend", "attended", "was attended" }),
        ("He ___ the answer, but he was too shy to speak.", "knew", new string[] { "knows", "knew", "knowing" }),
        ("By next year, I ___ in Manila for 10 years.", "will have lived", new string[] { "live", "lived", "will have lived" }),
        ("They ___ happily when the phone rang.", "were talking", new string[] { "talk", "talked", "were talking" }),
    };

    private static List<(string, string, string[])> hardQuestions = new List<(string, string, string[])>
    {
        ("If he ___ harder, he would have passed the exam.", "had studied", new string[] { "studied", "had studied", "was studying" }),
        ("The novel, which ___ in the 19th century, remains relevant today.", "was written", new string[] { "wrote", "was written", "written" }),
        ("By the time she arrived, the play ___ already ___", "had, started", new string[] { "has, started", "had, started", "was, starting" }),
        ("Had I known the truth, I ___ differently.", "would have acted", new string[] { "will act", "would act", "would have acted" }),
        ("The poem uses ___ to compare love to a rose.", "metaphor", new string[] { "simile", "metaphor", "hyperbole" }),
        ("She said that she ___ to the meeting the next day.", "would go", new string[] { "will go", "would go", "goes" }),
        ("The book ___ into several languages.", "has been translated", new string[] { "translated", "has been translated", "is translating" }),
        ("If only he ___ more careful, the accident could have been avoided.", "had been", new string[] { "was", "were", "had been" }),
        ("The main character is considered a ___ hero because of his flaws.", "tragic", new string[] { "comic", "tragic", "epic" }),
        ("The story’s ___ is the central idea or message it conveys.", "theme", new string[] { "plot", "theme", "setting" }),
        ("She wished she ___ spoken earlier.", "had", new string[] { "has", "had", "have" }),
        ("The students were made ___ quietly.", "to sit", new string[] { "sit", "to sit", "sitting" }),
        ("The painting was not only beautiful ___ also meaningful.", "but", new string[] { "and", "but", "or" }),
        ("If you ___ earlier, you could have caught the train.", "had left", new string[] { "left", "had left", "leaving" }),
        ("The climax of a story usually refers to its ___", "highest point of tension", new string[] { "beginning", "highest point of tension", "resolution" }),
        ("He insisted that she ___ present at the ceremony.", "be", new string[] { "is", "was", "be" }),
        ("The research paper must ___ by Friday.", "be submitted", new string[] { "submit", "submits", "be submitted" }),
        ("The novel’s ___ is revealed through its characters and events.", "moral", new string[] { "moral", "climax", "setting" }),
        ("The witness claimed that he ___ the suspect at the scene.", "had seen", new string[] { "saw", "had seen", "seen" }),
        ("The teacher suggested that he ___ more carefully.", "study", new string[] { "studies", "study", "studied" }),
        ("The protagonist’s downfall was caused by his ___", "hubris", new string[] { "hubris", "destiny", "redemption" }),
        ("The committee decided that the meeting ___ postponed.", "be", new string[] { "is", "be", "was" }),
        ("The hero’s journey is an example of a literary ___", "archetype", new string[] { "theme", "archetype", "symbol" }),
        ("If I ___ about the storm, I would have stayed at home.", "had known", new string[] { "knew", "had known", "know" }),
        ("The writer’s use of exaggeration is called ___", "hyperbole", new string[] { "irony", "hyperbole", "metaphor" }),
        ("The man denied that he ___ the documents.", "had stolen", new string[] { "steals", "stole", "had stolen" }),
        ("This passage is written in ___ voice.", "passive", new string[] { "active", "passive", "middle" }),
        ("The author employs ___ to give human qualities to objects.", "personification", new string[] { "personification", "alliteration", "assonance" }),
        ("By the end of this month, she ___ in this city for a decade.", "will have lived", new string[] { "has lived", "will live", "will have lived" }),
        ("The character who opposes the hero is called the ___", "antagonist", new string[] { "antagonist", "protagonist", "narrator" }),
        ("The teacher demanded that the essay ___ rewritten.", "be", new string[] { "is", "was", "be" }),
        ("Had it not ___ for her help, I would have failed.", "been", new string[] { "be", "was", "been" }),
        ("Irony often means the opposite of what is ___", "said", new string[] { "thought", "said", "imagined" }),
        ("The Shakespearean play Macbeth is a ___", "tragedy", new string[] { "comedy", "tragedy", "romance" }),
        ("If I were you, I ___ apologize immediately.", "would", new string[] { "will", "would", "shall" }),
        ("The final resolution of a story is called the ___", "denouement", new string[] { "climax", "conflict", "denouement" }),
        ("The villagers reported that the bridge ___ down.", "had fallen", new string[] { "falls", "fell", "had fallen" }),
        ("The lawyer insisted that the truth ___ revealed.", "be", new string[] { "is", "be", "was" }),
        ("The narrator of a story is also known as the ___ voice.", "narrative", new string[] { "author", "narrative", "dialogue" }),
        ("If she ___ harder, she might have succeeded.", "had tried", new string[] { "tried", "had tried", "tries" }),
        ("The expression 'as brave as a lion' is an example of ___", "simile", new string[] { "simile", "metaphor", "personification" }),
        ("The novel’s setting provides the ___ of time and place.", "background", new string[] { "background", "plot", "theme" }),
        ("The villagers claimed they ___ strange noises at night.", "had heard", new string[] { "heard", "have heard", "had heard" }),
        ("The phrase 'the wind whispered' is an example of ___", "personification", new string[] { "alliteration", "personification", "simile" }),
        ("The book would not have been published if the editor ___ it.", "had rejected", new string[] { "rejected", "rejects", "had rejected" }),
        ("She speaks as if she ___ everything.", "knew", new string[] { "knows", "knew", "known" }),
        ("The recurring symbol in a story is called a ___", "motif", new string[] { "motif", "theme", "plot" }),
        ("The teacher recommended that the student ___ more novels.", "read", new string[] { "reads", "read", "reading" }),
        ("The main character’s struggle against society is an example of ___ conflict.", "external", new string[] { "internal", "external", "personal" }),
        ("If the alarm ___, we would have woken up on time.", "had rung", new string[] { "rang", "rings", "had rung" }),
        ("The phrase 'jumped with joy' is an example of ___", "idiom", new string[] { "idiom", "hyperbole", "simile" }),
    };

    public static QuestionData GetQuestion(string type)
    {
        (string, string, string[]) q;

        if (type == "easy")
        {
            q = easyQuestions[Random.Range(0, easyQuestions.Count)];
        }
        else if (type == "medium")
        {
            q = medQuestions[Random.Range(0, medQuestions.Count)];
        }
        else if (type == "hard")
        {
            q = hardQuestions[Random.Range(0, hardQuestions.Count)];
        }
        else
        {
            Debug.LogWarning($"Unknown question type '{type}', defaulting to easy.");
            q = easyQuestions[Random.Range(0, easyQuestions.Count)];
        }

        string question = q.Item1;
        string correctAnswer = q.Item2;
        string[] allChoices = q.Item3;

        List<string> choiceList = new List<string>(allChoices);
        Shuffle(choiceList);

        int correctIndex = choiceList.IndexOf(correctAnswer);

        return new QuestionData
        {
            question = question,
            choices = choiceList.ToArray(),
            correctAnswerIndex = correctIndex
        };
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}
