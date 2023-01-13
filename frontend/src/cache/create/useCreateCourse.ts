import axios from 'axios';
import {
  useMutation,
  useQueryClient,
} from 'react-query'
import { Course } from '../../entities';



export const useCreateCourse = (success: (course: Course) => void, error: (e: any) => void) => {
  const queryClient = useQueryClient();
  return useMutation((req: any) => axios.post(`/courses`, req, {
    headers: {
      'Content-type': 'application/json',
    },
  }), {
    onSuccess: (e) => {
      success(e.data);
      queryClient.invalidateQueries([`getCourses`])
    },
    onError: (e: any) => {
      error(e.message);
    }
  })
}