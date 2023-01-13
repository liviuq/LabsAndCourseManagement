import axios from 'axios';
import {
  useMutation,
} from 'react-query'



export const useDeleteTeacherFromCourse = (courseId: string, teacherId: string, success: () => void, error: (e: string) => void) => {
  return useMutation(() => axios.delete(`/didactic/${teacherId}/${courseId}`), {
    onSuccess: () => {
      success()
    },
    onError: (e: any) => {
      error(e.message)
    }
  })
}