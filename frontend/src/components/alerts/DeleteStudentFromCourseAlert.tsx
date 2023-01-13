import { Text, AlertDialog, AlertDialogOverlay, AlertDialogContent, AlertDialogHeader, AlertDialogBody, Flex, AlertDialogFooter, Button } from '@chakra-ui/react'
import React, { useRef } from 'react'
import { RiErrorWarningLine } from 'react-icons/ri'
import { useQueryClient } from 'react-query';
import { useDeleteStudentFromCourse } from '../../cache/delete';
import { Course, Student } from '../../entities';
import { useToast } from '../../hooks';

interface DeleteStudentFromCourseAlertProps {
  course: Course | null;
  student: Student;
  isOpen: boolean;
  onClose: () => void;
}

export const DeleteStudentFromCourseAlert: React.FC<DeleteStudentFromCourseAlertProps> = ({ course, student, isOpen, onClose }) => {
  const cancelRef = useRef(null);
  const toast = useToast();
  const queryClient = useQueryClient();
  const deleteStudentFromCourse = useDeleteStudentFromCourse(
    course?.id as string,
    student.id,
    () => {
      toast({
        title: 'Student removed successfuly.',
        status: 'success',
      })
      queryClient.invalidateQueries([`getStudentsBy${course?.id}`])
      onClose()
    },
    (e) => {
      toast({
        title: e,
        status: 'error',
      })
    })
  const onSubmit = () => {
    deleteStudentFromCourse.mutate()
  }
  return (
    <AlertDialog
      isCentered
      isOpen={isOpen}
      leastDestructiveRef={cancelRef}
      onClose={onClose}
    >
      <AlertDialogOverlay>
        <AlertDialogContent>
          <AlertDialogHeader fontSize='lg' fontWeight='bold'>
            <Text>
              Remove
              <Text as="span" color="teal">
                {' ' + student.firstName + ' ' + student.lastName + ' '}
              </Text>
              from
              <Text as="span" color="teal">
                {' ' + course?.title}
              </Text>
            </Text>
          </AlertDialogHeader>

          <AlertDialogBody mb="25px">
            <Flex direction="column" w="full" align="center" gap="12px">
              <RiErrorWarningLine size="48px" color="Red" />
              <Text textAlign="center" fontWeight="bold" fontSize="24px">
                Are you sure?
              </Text>
              <Text textAlign="center" color="red">
                You can't undo this action afterwards.
              </Text>
            </Flex>
          </AlertDialogBody>

          <AlertDialogFooter>
            <Button ref={cancelRef} onClick={onClose}>
              Cancel
            </Button>
            <Button colorScheme='red' onClick={onSubmit} ml={3}>
              Remove
            </Button>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialogOverlay>
    </AlertDialog>
  )
}

