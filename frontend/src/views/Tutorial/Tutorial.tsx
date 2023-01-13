import { Button } from '@chakra-ui/react';
import React, { useState } from 'react'
import { useQueryClient } from 'react-query';
import { useNavigate } from 'react-router-dom';
import { TutorialAssignStudents } from './TutorialAssignStudents';
import { TutorialAssignTeachers } from './TutorialAssignTeachers';
import { TutorialCreateCourse } from './TutorialCreateCourse';
import { TutorialIntroduction } from './TutorialIntroduction';

export const Tutorial: React.FC = () => {
  const navigate = useNavigate();
  const [step, setStep] = useState<number>(0);
  const [courseId, setCourseId] = useState<string>('');
  const queryClient = useQueryClient();
  const finishTutorial = () => {
    queryClient.invalidateQueries([`getStudents`, `getTeachers`, `getTeachersBy${courseId}`, `getStudentsBy${courseId}`, `getCourseBy${courseId}`]);
    navigate(`/course/${courseId}`);
  }
  const getComponent = () => {
    switch (step) {
      case 0:
        return <TutorialIntroduction nextStep={() => setStep(prev => prev + 1)} />
      case 1:
        return <TutorialCreateCourse nextStep={() => setStep(prev => prev + 1)} setCourseId={setCourseId} />
      case 2:
        return <TutorialAssignTeachers nextStep={() => { setStep(prev => prev + 1) }} courseId={courseId} />
      case 3:
        return <TutorialAssignStudents nextStep={() => finishTutorial()} courseId={courseId} />
      default:
        <div>Wrong step</div>
    }
  }
  return <>{getComponent()}</>

}

