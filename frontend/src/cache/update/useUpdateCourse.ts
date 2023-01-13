import axios from 'axios';
import {
  useMutation,
  useQueryClient,
} from 'react-query'



export const useUpdateCourse = (courseId: string, success: () => void, error: (e: any) => void) => {
  const queryClient = useQueryClient();
  return useMutation((req: any) => axios.put(`/courses/${courseId}`, req, {
    headers: {
      'Content-type': 'application/json',
    },
  }), {
    onSuccess: (e) => {
      success();
      queryClient.invalidateQueries([`getCourses`])
    },
    onError: (e: any) => {
      error(e.message);
    }
  })
}