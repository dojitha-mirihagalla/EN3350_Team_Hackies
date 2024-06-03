export const jsQuizz = {
    questions: [
      {
        question:
          "What is the primary source of energy for most power grids around the world?",
        choices: [
          "A. Solar power",
          "B. Wind power",
          "C. Fossil fuels",
          "D. Hydropower",
        ],
        type: "MCQs",
        correctAnswer: "C. Fossil fuels",
      },
      {
        question: "How does electricity typically travel from power plants to consumers?",
        choices: [
          "A. Through water pipes",
          "B. Via transmission and distribution networks",
          "C. Directly from generators to homes",
          "D. Through the internet",
        ],
        type: "MCQs",
        correctAnswer: "B. Via transmission and distribution networks",
      },
      {
        question:
          "Why is energy efficiency important in homes and businesses?",
        choices: ["A. It increases energy consumption", "B. It leads to higher energy costs", "C. It reduces energy bills and environmental impact", "D. It has no impact on the environment"],
        type: "MCQs",
        correctAnswer: "C. It reduces energy bills and environmental impact",
      },
      {
        question: "What is the primary goal of demand management in energy usage?",
        choices: ["A. To increase the overall energy consumption", "B. To balance energy supply and demand", "C. To eliminate the use of renewable energy sources", "D. To double the energy costs for consumers"],
        type: "MCQs",
        correctAnswer: "B. To balance energy supply and demand",
      },
      {
        question: "Which of the following is a common method used in demand management to encourage lower energy use during peak hours?",
        choices: [
          "A. Increasing energy prices during off-peak hours",
          "B. Providing incentives for high energy consumption",
          "C. Offering lower rates or incentives for using less energy during peak times",
          "D. Discouraging the use of energy-efficient appliances",
        ],
        type: "MCQs",
        correctAnswer: "C. Offering lower rates or incentives for using less energy during peak times",
      },
      {
        question:
          "Benefits to the consumer from demand management include:",
        choices: [
          "A. Higher energy bills",
          "B. Less control over their energy use",
          "C. Savings on their electricity bill",
          "D. Reduced internet connectivity",
        ],
        type: "MCQs",
        correctAnswer: "C. Savings on their electricity bill",
      },
      {
        question:
          "How does implementing demand management strategies benefit the environment?",
        choices: [
          "A. By significantly increasing carbon emissions",
          "B. By reducing reliance on fossil fuels and lowering carbon emissions",
          "C. By eliminating the need for public transportation",
          "D. By discouraging the use of renewable energy",
        ],
        type: "MCQs",
        correctAnswer: "B. By reducing reliance on fossil fuels and lowering carbon emissions",
      },
      {
        question:
          "What can be a direct benefit of participating in a demand management program for consumers?",
        choices: [
          "A. Higher energy bills",
          "B. Less control over their energy use",
          "C. Savings on their electricity bill",
          "D. Reduced internet connectivity",
        ],
        type: "MCQs",
        correctAnswer: "C. Savings on their electricity bill",
      },
      {
        question:
          "Why is load shifting important in demand management?",
        choices: [
          "A. It increases the energy load during peak times",
          "B. It shifts energy usage to times when demand is higher",
          "C. It helps balance the power grid by using energy during lower-demand periods",
          "D. It makes energy more expensive during off-peak hours",
        ],
        type: "MCQs",
        correctAnswer: "C. It helps balance the power grid by using energy during lower-demand periods",
      },
      {
        question:
          "Which of the following electric loads can be effectively managed as part of a demand management program?",
        choices: [
          "A. Fixed lighting systems in public areas",
          "B. Emergency medical equipment",
          "C. Residential air conditioning units",
          "D. Data centers that require constant cooling",
        ],
        type: "MCQs",
        correctAnswer: "C. Residential air conditioning units",
      }
    ],
  };
  
  export const resultInitalState = {
    score: 0,
    correctAnswers: 0,
    wrongAnswers: 0,
  };

export const correctAnswer = {
  Questions:[
    {
      Question: "What is the primary source of energy for most power grids around the world?",
      Answer: "Fossil fuels",
      G_feedback:"While the mix of energy sources varies by region, fossil fuels remain the dominant source for electricity generation globally, though renewable sources are on the rise.",
      c_answer:"",
      s_feedback:[
        "Solar power is growing but is not the primary source globally.",
        "Wind power is significant in some areas but not the main source worldwide.",
        "Correct! Fossil fuels, including coal, natural gas, and oil, are currently the primary energy source for most power grids.",
        "Hydropower is a key renewable source but not the primary source globally.",
      ],
      s_index: ""
    },
    {
      Question: "How does electricity typically travel from power plants to consumers?",
      Answer: "Via transmission and distribution networks",
      G_feedback:"Electricity is generated at power plants and then transmitted over long distances via high-voltage transmission lines. It's then distributed to consumers through lower-voltage distribution networks.",
      c_answer:"",
      s_feedback:[
        "Water pipes are used for plumbing, not electrical transmission.",
        "Correct! Transmission and distribution networks are essential for delivering electricity from power plants to consumers.",
        "Electricity must be transmitted and distributed over networks; it doesn't go directly from generators to homes.",
        "The internet is a network for data, not electricity.",
      ],
      s_index: ""
    },
    {
      Question: "Why is energy efficiency important in homes and businesses?",
      Answer: "It reduces energy bills and environmental impact",
      G_feedback:"Energy efficiency is crucial for reducing energy consumption, lowering energy bills, and minimizing the environmental footprint by decreasing greenhouse gas emissions.",
      c_answer:"",
      s_feedback:[
        "Energy efficiency aims to reduce, not increase, consumption.",
        "The goal of energy efficiency is to lower costs, not raise them.",
        "Correct! Energy efficiency helps in saving on energy bills and reducing the environmental impact.",
        "Energy efficiency has a significant positive impact on the environment by reducing emissions.",
      ],
      s_index: ""
    },
    {
      Question: "What is the primary goal of demand management in energy usage?",
      Answer: "To balance energy supply and demand",
      G_feedback:"Demand management aims to ensure energy is used more efficiently, balancing the supply with the fluctuating demand to maintain grid stability and reduce costs.",
      c_answer:"",
      s_feedback:[
        "This is the opposite of demand management's goal, which aims to optimize, not increase, energy use.",
        "Correct! Balancing energy supply and demand helps improve grid reliability and efficiency.",
        "Demand management often encourages the integration of renewable energy sources, not their elimination.",
        "The goal is to potentially lower or optimize costs, not increase them.",
      ],
      s_index:""
    },
    {
      Question: "Which of the following is a common method used in demand management to encourage lower energy use during peak hours?",
      Answer: "Offering lower rates or incentives for using less energy during peak times",
      G_feedback:"Lowering rates or providing incentives for reduced energy use during peak hours helps smooth out energy demand, benefiting both the grid and consumer",
      c_answer:"",
      s_feedback:[
        "This approach would not encourage lower usage during peak times.",
        "Incentives are typically offered for reducing consumption, not increasing it.",
        "Correct! Incentives for lower usage during peak hours help manage demand effectively.",
        "Energy-efficient appliances are actually encouraged as part of demand management strategies.",
      ],
      s_index: ""
    },
    {
      Question: "Benefits to the consumer from demand management include:",
      Answer: "Savings on their electricity bill",
      G_feedback:"Participating in demand management programs can lead to significant savings on electricity bills for consumers by incentivizing energy use during off-peak hours.",
      c_answer:"",
      s_feedback:[
        "Demand management aims to reduce, not increase, consumer energy bills.",
        "It actually offers more control over energy use and costs.",
        "Correct! One of the key benefits for consumers is the potential for savings on their electricity bills.",
        "Demand management focuses on energy consumption, not affecting internet connectivity.",
      ],
      s_index: ""
    },
    {
      Question: "How does implementing demand management strategies benefit the environment?",
      Answer: "By reducing reliance on fossil fuels and lowering carbon emissions",
      G_feedback:"Implementing demand management strategies plays a crucial role in environmental conservation by reducing the reliance on non-renewable energy sources and minimizing carbon emissions.",
      c_answer:"",
      s_feedback:[
        "Demand management aims to decrease, not increase, carbon emissions.",
        "Correct! Reducing reliance on fossil fuels and lowering carbon emissions are key environmental benefits of demand management.",
        "Demand management strategies do not involve transportation policies directly.",
        "These strategies typically encourage, rather than discourage, the use of renewable energy sources.",
      ],
      s_index: ""
    },
    {
      Question: "What can be a direct benefit of participating in a demand management program for consumers?",
      Answer: "Savings on their electricity bill",
      G_feedback:"Participation in demand management programs often results in direct benefits for consumers, such as savings on electricity bills, by encouraging energy use during less expensive, off-peak hours.",
      c_answer:"",
      s_feedback:[
        "The goal of demand management is to offer savings, not to increase bills.",
        "Participants typically gain greater control and flexibility over their energy use.",
        "Correct! Saving on electricity bills is a significant benefit for consumers who participate in demand management programs.",
        "Demand management does not impact internet connectivity.",
      ],
      s_index: ""
    },
    {
      Question: "Why is load shifting important in demand management?",
      Answer: "It helps balance the power grid by using energy during lower-demand periods",
      G_feedback:"Load shifting is a critical component of demand management, aimed at moving energy use from peak to off-peak hours. This helps balance the power grid, reduces the need for additional power plants, and can lead to cost savings for consumers and utility providers alike.",
      c_answer:"",
      s_feedback:[
        "The purpose of load shifting is to decrease, not increase, the load during peak times to help balance energy demand.",
        "Shifting energy usage to higher demand times would counteract the goals of demand management, which seeks to alleviate these peaks.",
        "Correct! By shifting energy use to lower-demand periods, we can achieve a more balanced and efficient use of the power grid.",
        "Load shifting is designed to take advantage of lower costs during off-peak hours, not to make energy more expensive.",
      ],
      s_index: ""
    },
    {
      Question: "Which of the following electric loads can be effectively managed as part of a demand management program?",
      Answer: "Residential air conditioning units",
      G_feedback:"Demand management programs focus on adjusting the usage of flexible and non-critical electric loads to optimize energy consumption. Residential air conditioning units, for example, can be adjusted without compromising safety or critical operations, making them ideal for inclusion in these programs.",
      c_answer:"",
      s_feedback:[
        "While lighting can be managed, fixed systems in public areas often have safety implications that limit their flexibility.",
        "Emergency medical equipment is critical and cannot be subject to demand management due to the risk to human life.",
        "Correct! Residential air conditioning units are a significant and flexible load that can be adjusted to enhance grid efficiency without compromising comfort significantly.",
        "Data centers have strict cooling requirements for operational integrity and may not offer the flexibility required for effective demand management.",
      ],
      s_index: ""
    },
  ],
};

