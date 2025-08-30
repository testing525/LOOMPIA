using UnityEngine;
using System.Collections.Generic;

public static class ScienceQuestionProvider
{
    //1st str question
    //2nd string answer
    //3rd string multiple choices(include the correct answer, the other 2 random but related)
    private static List<(string, string, string[])> easyQuestions = new List<(string, string, string[])>
    {
        ("Which planet do we live on?", "Earth", new string[] { "Earth", "Mars", "Venus" }),
        ("What is the center of our solar system?", "Sun", new string[] { "Moon", "Sun", "Earth" }),
        ("Which gas do humans need to survive?", "Oxygen", new string[] { "Oxygen", "Carbon Dioxide", "Hydrogen" }),
        ("Which animal says 'meow'?", "Cat", new string[] { "Dog", "Cat", "Cow" }),
        ("Which part of the plant makes food?", "Leaf", new string[] { "Leaf", "Root", "Stem" }),
        ("How many legs does a spider have?", "8", new string[] { "6", "8", "10" }),
        ("What do bees make?", "Honey", new string[] { "Milk", "Honey", "Juice" }),
        ("What do cows give us to drink?", "Milk", new string[] { "Milk", "Juice", "Water" }),
        ("Which animal is the largest on Earth?", "Blue Whale", new string[] { "Elephant", "Blue Whale", "Shark" }),
        ("What do humans use to see?", "Eyes", new string[] { "Ears", "Eyes", "Nose" }),
        ("Which planet is called the Red Planet?", "Mars", new string[] { "Venus", "Mars", "Jupiter" }),
        ("What do we call water that falls from the clouds?", "Rain", new string[] { "Snow", "Rain", "Fog" }),
        ("Which organ pumps blood in our body?", "Heart", new string[] { "Lungs", "Heart", "Brain" }),
        ("Which part of the body helps us smell?", "Nose", new string[] { "Ears", "Nose", "Hands" }),
        ("How many days are in one week?", "7", new string[] { "5", "6", "7" }),
        ("Which star lights up the Earth?", "Sun", new string[] { "Moon", "Sun", "Venus" }),
        ("What is frozen water called?", "Ice", new string[] { "Snow", "Ice", "Rain" }),
        ("Which bird can swim but cannot fly?", "Penguin", new string[] { "Eagle", "Penguin", "Ostrich" }),
        ("What color is the sky on a clear day?", "Blue", new string[] { "Blue", "Green", "Red" }),
        ("What do plants need to grow?", "Sunlight", new string[] { "Sunlight", "Sand", "Rocks" }),
        ("Which shape is a wheel?", "Circle", new string[] { "Square", "Circle", "Triangle" }),
        ("Which insect has colorful wings?", "Butterfly", new string[] { "Ant", "Bee", "Butterfly" }),
        ("Which is the fastest land animal?", "Cheetah", new string[] { "Horse", "Cheetah", "Tiger" }),
        ("How many teeth does an adult human usually have?", "32", new string[] { "30", "32", "28" }),
        ("Which part of the body helps us think?", "Brain", new string[] { "Heart", "Brain", "Lungs" }),
        ("Which planet is the largest?", "Jupiter", new string[] { "Earth", "Mars", "Jupiter" }),
        ("Which part of the body helps us hear?", "Ears", new string[] { "Ears", "Eyes", "Mouth" }),
        ("Which bird lays the largest egg?", "Ostrich", new string[] { "Chicken", "Ostrich", "Duck" }),
        ("What is the baby of a dog called?", "Puppy", new string[] { "Puppy", "Kitten", "Calf" }),
        ("Which gas do plants release?", "Oxygen", new string[] { "Oxygen", "Carbon Dioxide", "Helium" }),
        ("How many legs does an insect have?", "6", new string[] { "4", "6", "8" }),
        ("What do you call hot melted rock from a volcano?", "Lava", new string[] { "Stone", "Lava", "Clay" }),
        ("What do fish use to breathe?", "Gills", new string[] { "Nose", "Lungs", "Gills" }),
        ("Which planet is closest to the Sun?", "Mercury", new string[] { "Mercury", "Earth", "Mars" }),
        ("What do birds use to fly?", "Wings", new string[] { "Wings", "Legs", "Fins" }),
        ("How many colors are in a rainbow?", "7", new string[] { "6", "7", "8" }),
        ("What is H2O commonly called?", "Water", new string[] { "Water", "Salt", "Oxygen" }),
        ("Which planet is called the Evening Star?", "Venus", new string[] { "Mars", "Venus", "Saturn" }),
        ("What is the hardest natural substance?", "Diamond", new string[] { "Gold", "Iron", "Diamond" }),
        ("Which part of the body helps us taste?", "Tongue", new string[] { "Eyes", "Nose", "Tongue" }),
        ("What do you call young frogs?", "Tadpoles", new string[] { "Tadpoles", "Puppies", "Kittens" }),
        ("Which animal is called the King of the Jungle?", "Lion", new string[] { "Tiger", "Lion", "Cheetah" }),
        ("Which bird is known for saying 'Polly wants a cracker'?", "Parrot", new string[] { "Parrot", "Sparrow", "Owl" }),
        ("What do camels store in their humps?", "Fat", new string[] { "Water", "Fat", "Milk" }),
        ("What shape is the Earth?", "Round", new string[] { "Square", "Round", "Flat" }),
        ("Which organ helps us breathe?", "Lungs", new string[] { "Heart", "Lungs", "Kidney" }),
        ("Which insect makes silk?", "Silkworm", new string[] { "Bee", "Silkworm", "Ant" }),
        ("Which star do we see at night?", "Moon", new string[] { "Moon", "Sun", "Venus" }),
        ("What is the only natural satellite of Earth?", "Moon", new string[] { "Moon", "Mars", "Venus" }),
        ("What gives plants their green color?", "Chlorophyll", new string[] { "Chlorophyll", "Water", "Soil" }),
    };

    private static List<(string, string, string[])> medQuestions = new List<(string, string, string[])>
    {
        ("Which planet has rings around it?", "Saturn", new string[] { "Saturn", "Mars", "Venus" }),
        ("What force pulls objects toward Earth?", "Gravity", new string[] { "Magnetism", "Gravity", "Friction" }),
        ("Which blood cells fight infections?", "White blood cells", new string[] { "Red blood cells", "White blood cells", "Platelets" }),
        ("What gas do plants take in during photosynthesis?", "Carbon Dioxide", new string[] { "Oxygen", "Carbon Dioxide", "Nitrogen" }),
        ("What is the boiling point of water in Celsius?", "100", new string[] { "50", "100", "150" }),
        ("Which part of the plant carries water from roots to leaves?", "Stem", new string[] { "Stem", "Flower", "Leaf" }),
        ("What is the largest planet in our solar system?", "Jupiter", new string[] { "Mars", "Saturn", "Jupiter" }),
        ("Which blood cells carry oxygen?", "Red blood cells", new string[] { "Platelets", "White blood cells", "Red blood cells" }),
        ("What is the chemical symbol for gold?", "Au", new string[] { "Ag", "Au", "Gd" }),
        ("What organ in humans helps filter blood?", "Kidney", new string[] { "Kidney", "Heart", "Liver" }),
        ("Which vitamin do we get from sunlight?", "Vitamin D", new string[] { "Vitamin A", "Vitamin B", "Vitamin D" }),
        ("Which part of the brain controls balance?", "Cerebellum", new string[] { "Cerebellum", "Cerebrum", "Medulla" }),
        ("Which planet is known for its big red spot?", "Jupiter", new string[] { "Mars", "Saturn", "Jupiter" }),
        ("What is the freezing point of water in Celsius?", "0", new string[] { "0", "10", "32" }),
        ("What is the process by which plants make food?", "Photosynthesis", new string[] { "Respiration", "Photosynthesis", "Digestion" }),
        ("Which human organ is responsible for breathing?", "Lungs", new string[] { "Lungs", "Liver", "Heart" }),
        ("What is the fastest bird?", "Peregrine Falcon", new string[] { "Ostrich", "Eagle", "Peregrine Falcon" }),
        ("Which part of the plant makes seeds?", "Flower", new string[] { "Root", "Stem", "Flower" }),
        ("Which blood type is called the universal donor?", "O-", new string[] { "A", "O-", "AB+" }),
        ("What part of the Earth is made of liquid metal?", "Outer Core", new string[] { "Crust", "Mantle", "Outer Core" }),
    };

    private static List<(string, string, string[])> hardQuestions = new List<(string, string, string[])>
    {
        ("What is the hardest natural substance on Earth?", "Diamond", new string[] { "Gold", "Diamond", "Iron" }),
        ("Which planet has the most moons?", "Saturn", new string[] { "Mars", "Jupiter", "Saturn" }),
        ("What process in plants produces oxygen?", "Photosynthesis", new string[] { "Respiration", "Photosynthesis", "Digestion" }),
        ("Which part of the cell controls activities?", "Nucleus", new string[] { "Nucleus", "Membrane", "Cytoplasm" }),
        ("What gas do plants absorb during photosynthesis?", "Carbon Dioxide", new string[] { "Oxygen", "Carbon Dioxide", "Nitrogen" }),
        ("Which planet is the largest in our solar system?", "Jupiter", new string[] { "Saturn", "Jupiter", "Neptune" }),
        ("What is the boiling point of water at sea level?", "100°C", new string[] { "0°C", "50°C", "100°C" }),
        ("Which part of the human brain controls balance?", "Cerebellum", new string[] { "Cerebellum", "Cerebrum", "Medulla" }),
        ("What is H2O commonly known as?", "Water", new string[] { "Water", "Oxygen", "Hydrogen" }),
        ("What do you call animals that eat both plants and meat?", "Omnivores", new string[] { "Herbivores", "Carnivores", "Omnivores" }),
        ("What part of Earth is made up of molten rock?", "Mantle", new string[] { "Crust", "Mantle", "Core" }),
        ("Which force pulls objects toward Earth?", "Gravity", new string[] { "Friction", "Gravity", "Magnetism" }),
        ("What do we call stars that explode at the end of their lives?", "Supernova", new string[] { "Meteor", "Supernova", "Comet" }),
        ("Which planet is tilted on its side?", "Uranus", new string[] { "Uranus", "Earth", "Mercury" }),
        ("What is the main source of the Sun's energy?", "Nuclear Fusion", new string[] { "Combustion", "Nuclear Fusion", "Radiation" }),
        ("What vitamin do we get from sunlight?", "Vitamin D", new string[] { "Vitamin C", "Vitamin D", "Vitamin A" }),
        ("Which blood cells fight infection?", "White Blood Cells", new string[] { "Red Blood Cells", "White Blood Cells", "Platelets" }),
        ("Which organ breaks down food with acid?", "Stomach", new string[] { "Stomach", "Liver", "Kidney" }),
        ("Which ocean is the largest on Earth?", "Pacific Ocean", new string[] { "Atlantic Ocean", "Indian Ocean", "Pacific Ocean" }),
        ("What gas makes up most of Earth’s atmosphere?", "Nitrogen", new string[] { "Oxygen", "Carbon Dioxide", "Nitrogen" }),
        ("What is the only planet known to support life?", "Earth", new string[] { "Earth", "Mars", "Venus" }),
        ("Which human organ pumps blood?", "Heart", new string[] { "Liver", "Heart", "Lungs" }),
        ("What tool is used to measure temperature?", "Thermometer", new string[] { "Barometer", "Thermometer", "Speedometer" }),
        ("Which energy source is renewable?", "Solar", new string[] { "Coal", "Solar", "Oil" }),
        ("What kind of star is the Sun?", "Yellow Dwarf", new string[] { "Red Giant", "Yellow Dwarf", "White Dwarf" }),
        ("What is the largest internal organ in the human body?", "Liver", new string[] { "Heart", "Liver", "Lung" }),
        ("What part of the plant carries water from roots to leaves?", "Xylem", new string[] { "Xylem", "Phloem", "Stomata" }),
        ("Which scientist proposed the theory of gravity?", "Isaac Newton", new string[] { "Albert Einstein", "Isaac Newton", "Galileo Galilei" }),
        ("What is Earth’s only natural satellite?", "Moon", new string[] { "Moon", "Io", "Europa" }),
        ("What instrument is used to see very small objects?", "Microscope", new string[] { "Microscope", "Telescope", "Periscope" }),
        ("What causes tides on Earth?", "Moon’s Gravity", new string[] { "Sunlight", "Moon’s Gravity", "Wind" }),
        ("What is the powerhouse of the cell?", "Mitochondria", new string[] { "Mitochondria", "Nucleus", "Ribosome" }),
        ("What is the main gas found in balloons?", "Helium", new string[] { "Helium", "Hydrogen", "Oxygen" }),
        ("Which part of the plant makes food?", "Leaves", new string[] { "Leaves", "Roots", "Stem" }),
        ("Which process turns liquid water into gas?", "Evaporation", new string[] { "Condensation", "Evaporation", "Freezing" }),
        ("What do bees collect from flowers?", "Nectar", new string[] { "Nectar", "Pollen", "Sap" }),
        ("Which layer of the Earth is liquid?", "Outer Core", new string[] { "Inner Core", "Crust", "Outer Core" }),
        ("Which natural satellite orbits Earth?", "Moon", new string[] { "Moon", "Titan", "Europa" }),
        ("What is the largest type of whale?", "Blue Whale", new string[] { "Blue Whale", "Orca", "Sperm Whale" }),
        ("Which scientist is known as the father of modern physics?", "Albert Einstein", new string[] { "Isaac Newton", "Albert Einstein", "Galileo Galilei" }),
        ("What is the most abundant element in the universe?", "Hydrogen", new string[] { "Oxygen", "Hydrogen", "Helium" }),
        ("Which organ filters blood in the human body?", "Kidney", new string[] { "Liver", "Kidney", "Lungs" }),
        ("What protects Earth from harmful solar radiation?", "Ozone Layer", new string[] { "Ozone Layer", "Clouds", "Mountains" }),
        ("What is the largest bone in the human body?", "Femur", new string[] { "Femur", "Skull", "Spine" }),
        ("What causes the seasons on Earth?", "Tilt of Earth’s Axis", new string[] { "Rotation", "Tilt of Earth’s Axis", "Gravity" }),
        ("Which organ is responsible for breathing?", "Lungs", new string[] { "Lungs", "Kidney", "Liver" }),
        ("What is the fastest land animal?", "Cheetah", new string[] { "Cheetah", "Horse", "Tiger" }),
        ("Which organelle in plants contains chlorophyll?", "Chloroplast", new string[] { "Chloroplast", "Nucleus", "Mitochondria" }),
        ("What causes day and night on Earth?", "Earth’s Rotation", new string[] { "Earth’s Orbit", "Earth’s Rotation", "Moon’s Gravity" }),
        ("What is the study of fossils called?", "Paleontology", new string[] { "Paleontology", "Archaeology", "Geology" })
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
