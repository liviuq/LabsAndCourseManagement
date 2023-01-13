import axios from "axios";
import { Teacher } from "../../entities";

export const useGetTeachersByCourseId = async (courseId: string) => {
  const { data } = await axios.get<Teacher[]>(`/didactic/course/${courseId}/teachers`)
  return await data;
}