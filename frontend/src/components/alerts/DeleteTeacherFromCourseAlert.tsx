import { Text, AlertDialog, AlertDialogOverlay, AlertDialogContent, AlertDialogHeader, AlertDialogBody, Flex, AlertDialogFooter, Button } from '@chakra-ui/react'
import React, { useRef } from 'react'
import { RiErrorWarningLine } from 'react-icons/ri'
import { useQueryClient } from 'react-query';
import { useDeleteTeacherFromCourse } from '../../cache/delete';
import { Course, Teacher } from '../../entities';
import { useToast } from '../../hooks';

interface DeleteTeacherFromCourseAlertProps {
  course: Course | null;
  teacher: Teacher;
  isOpen: boolean;
  onClose: () => void;
}

export const DeleteTeacherFromCourseAlert: React.FC<DeleteTeacherFromCourseAlertProps> = ({ course, teacher, isOpen, onClose }) => {
  const cancelRef = useRef(null);
  const toast = useToast();
  const queryClient = useQueryClient();
  const deleteTeacherFromCourse = useDeleteTeacherFromCourse(
    course?.id as string,
    teacher.id,
    () => {
      toast({
        title: 'Teacher removed successfuly.',
        status: 'success',
      })
      queryClient.invalidateQueries([`getTeachersBy${course?.id}`])
      onClose()
    },
    (e) => {
      toast({
        title: e,
        status: 'error',
      })
    })
  const onSubmit = () => {
    deleteTeacherFromCourse.mutate()
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
                {' ' + teacher.firstName + ' ' + teacher.lastName + ' '}
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

