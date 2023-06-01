import React, { useEffect, useState } from 'react';
import { Button, Card, CardActions, CardContent, CardHeader, Grid, Typography, RadioGroup, FormControlLabel, Radio } from '@mui/material';
import ScoreComponent from './ScoreComponent';

interface Question {
  id: number;
  questionText: string;
  options: string[];
}

interface Answer {
  questionId: number;
  userAnswer: number;
}

const QuizComponent: React.FC = () => {
  const [question, setQuestion] = useState<Question | null>(null);
  const [selectedOption, setSelectedOption] = useState<number | null>(null);
  const [quizName, setQuizName] = useState<string | null>(null);
  const [score, setScore] = useState<number>(0);

    // Henter spørsmålet når komponenten lastes inn
  useEffect(() => {
    fetch('http://localhost:5236/quiz')
      .then(response => response.json())
      .then(data => {
        setQuestion(data);
      })
      .catch(error => console.error('Error:', error));
  }, []);
 
  // Henter quiz-tittelen når komponenten lastes inn
  useEffect(() => {
    fetch('http://localhost:5236/quiz/title')
      .then(response => response.json())
      .then(data => {
        setQuizName(data.title);
      })
      .catch(error => console.error('Error:', error));
  }, []);

    // Håndterer innsending av svar
  const handleSubmit = () => {
    if (selectedOption === null || question === null) {
      alert('Please select an option');
      return;
    }
    
   // Oppretter svaret
    const answer: Answer = {
      questionId: question.id,
      userAnswer: selectedOption,
    };
  
    // Sender svaret til serveren
    fetch('http://localhost:5236/quiz', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(answer),
    })
      .then(response => {
      // Hvis svaret er riktig, hent neste spørsmål og øk score
        if (response.ok) {
          return response.json();
        } else {
          throw new Error('Incorrect Answer');
        }
      })
      .then(data => {
        if (data.questionText) {
          setScore(prevScore => prevScore + 1); // Increment the score
          setQuestion(data);
        } else {
          alert('Incorrect Answer!');
        }
      })
      .catch(error => {
        console.error('Error:', error);
        alert('Incorrect Answer!');
      });
  };
  

  return (
    <Grid container justifyContent="center">
      <Grid item xs={12} sm={8} md={6}>
        <Card>
          <CardHeader title={quizName || 'Loading...'} />
          <CardContent>
            {question ? (
              <>
                <Typography variant="h5">{question.questionText}</Typography>
                <RadioGroup value={selectedOption} onChange={e => setSelectedOption(Number(e.target.value))}>
                  {question.options.map((option, index) => (
                    <FormControlLabel key={index} value={index} control={<Radio />} label={option} />
                  ))}
                </RadioGroup>
              </>
            ) : (
              <Typography>Loading...</Typography>
            )}
          </CardContent>
          <CardActions>
            <Button variant="contained" color="primary" onClick={handleSubmit}>
              Submit
            </Button>
          </CardActions>
        </Card>
        <ScoreComponent score={score} />
      </Grid>
    </Grid>
  );
};

export default QuizComponent;
