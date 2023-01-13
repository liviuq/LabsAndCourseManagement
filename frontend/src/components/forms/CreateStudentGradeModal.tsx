import { Modal, ModalOverlay, ModalContent, ModalHeader, ModalCloseButton, ModalBody, Flex, FormControl, FormLabel, Input, NumberInput, NumberInputField, ModalFooter, Button } from '@chakra-ui/react'
import Axios from 'axios';
import React, { useState } from 'react'
import { useCreateCourse, useCreateGrade } from '../../cache/create';
import { CreateCourse, CreateGrade } from '../../entities/Create';
import { useToast } from '../../hooks';

interface CreateStudentGradeModalProps {
  studentId: string;
  courseId: string;
  isOpen: boolean;
  setIsOpen: (isOpen: boolean) => void;
}

export const CreateStudentGradeModal: React.FC<CreateStudentGradeModalProps> = ({ isOpen, setIsOpen, studentId, courseId }) => {
  const toast = useToast();
  const [createGradeInput, setCreateGradeInput] = useState<CreateGrade>(CreateGrade.of({}));
  const createGrade = useCreateGrade(
    studentId,
    courseId
    , () => {
      toast({
        title: 'Grade added successfuly.',
        status: 'success',
      })
      setCreateGradeInput(CreateGrade.of({}))
      setIsOpen(false);
    }, (e: any) =>
    toast({
      title: e,
      status: 'error',
    })
  );
  const handleSubmit = (e: any) => {
    e.preventDefault()
    createGrade.mutate(createGradeInput)
  }

  return (
    <Modal isCentered size="xl" isOpen={isOpen} onClose={() => setIsOpen(false)}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>Add grade</ModalHeader>
        <ModalCloseButton />
        <form onSubmit={handleSubmit}>
          <ModalBody>
            <Flex direction="column" alignItems="center" px="32px" py="10px" gap="6px">
              <FormControl mb="6">
                <FormLabel m="0">Value</FormLabel>
                <NumberInput min={0} variant="flushed" >
                  <NumberInputField required onChange={(event) => {
                    setCreateGradeInput(prev => ({ ...prev, value: parseInt(event.target.value) }))
                  }} fontSize="lg" textColor="#00ABB3" px="10px" placeholder='Value' _placeholder={{ color: "#00ABB3" }} />
                </NumberInput>
              </FormControl>
            </Flex>
          </ModalBody>

          <ModalFooter >
            <Button onClick={() => setIsOpen(false)} size="md" variant="solid" mr="10px">Cancel</Button>
            <Button type="submit" size="md" variant="solid" colorScheme="teal">Submit</Button>
          </ModalFooter>
        </form>

      </ModalContent>
    </Modal >
  )
}

