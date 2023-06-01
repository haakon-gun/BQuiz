import React from 'react';
import { Card, CardContent, Typography } from '@mui/material';

interface ScoreComponentProps {
  score: number;
}

const ScoreComponent: React.FC<ScoreComponentProps> = ({ score }) => {
  return (
    <Card>
      <CardContent>
        <Typography variant="h5">Score: {score}</Typography>
      </CardContent>
    </Card>
  );
};

export default ScoreComponent;
