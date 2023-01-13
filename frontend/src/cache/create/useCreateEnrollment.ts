import axios from 'axios';
import {
  useMutation,
  useQueryClient,
} from 'react-query'



export const useCreateEnrollment = (increment: () => void, courseId: string) => {
  const queryClient = useQueryClient();
  return useMutation((req: any) => axios.post(`/enrollment/student/${req.id}/course/${courseId}`, {
    headers: {
      'Content-type': 'application/json',
    },
  }), {
    onSuccess: () => {
      queryClient.invalidateQueries([`getStudentsBy${courseId}`])
    },
    onError: () => {
      increment()
    }
  })
}