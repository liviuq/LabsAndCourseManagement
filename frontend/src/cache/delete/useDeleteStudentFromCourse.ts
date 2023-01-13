import axios from 'axios';
import {
  useMutation,
} from 'react-query'



export const useDeleteStudentFromCourse = (courseId: string, studentId: string, success: () => void, error: (e: string) => void) => {
  return useMutation(() => axios.delete(`/enrollment/${studentId}/${courseId}`), {
    onSuccess: () => {
      
      success()
    },
    onError: (e: any) => {
      error(e.message)
    }
  })
}