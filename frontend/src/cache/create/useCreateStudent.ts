import axios from 'axios';
import {
  useMutation,
  useQueryClient,
} from 'react-query'



export const useCreateStudent = (success: () => void, error: (e: any) => void) => {
  const queryClient = useQueryClient();
  return useMutation((req: any) => axios.post(`/students`, req, {
    headers: {
      'Content-type': 'application/json',
    },
  }), {
    onSuccess: (e) => {
      success();
      queryClient.invalidateQueries([`getStudents`])
    },
    onError: (e: any) => {
      error(e.message);
    }
  })
}