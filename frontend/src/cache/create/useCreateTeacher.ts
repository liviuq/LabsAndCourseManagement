import axios from 'axios';
import {
  useMutation,
  useQueryClient,
} from 'react-query'



export const useCreateTeacher = (success: () => void, error: (e: any) => void) => {
  const queryClient = useQueryClient();
  return useMutation((req: any) => axios.post(`/teachers`, req, {
    headers: {
      'Content-type': 'application/json',
    },
  }), {
    onSuccess: (e) => {
      success();
      queryClient.invalidateQueries([`getTeachers`])
    },
    onError: (e: any) => {
      error(e.message);
    }
  })
}