import axios from "axios";
import { Student } from "../../entities";

export const useGetStudentsByCourseId = async (courseId: string) => {
  const { data } = await axios.get<Student[]>(`/enrollment/course/${courseId}/students`)
  return await data;
}