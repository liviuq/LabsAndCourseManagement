import axios from 'axios';
import {
  useMutation,
  useQueryClient,
} from 'react-query'



export const useCreateGrade = (studentId: string, courseId: string, success: () => void, error: (e: any) => void) => {
  const queryClient = useQueryClient();
  return useMutation((req: any) => axios.post(`/grades/student/${studentId}/course/${courseId}`, req, {
    headers: {
      'Content-type': 'application/json',
    },
  }), {
    onSuccess: () => {
      success();
      queryClient.invalidateQueries([`getStudentsBy${courseId}`])
    },
    onError: (e: any) => {
      error(e);
    }
  })
}