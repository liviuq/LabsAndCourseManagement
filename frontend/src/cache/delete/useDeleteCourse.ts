import axios from "axios";

export const useDeleteCourse = (id: string, success: () => void, error: (e: any) => void) => {
  axios.delete(`/courses/${id}`).then(response => {
    success();
  }).catch(err => {
    error(err.message);
  })
}
