import axios from "axios";
import { Course } from "../../entities";

export const useGetCourseById = async (courseId: string) => {
  const { data } = await axios.get<Course>(`/courses/${courseId}`)
  return await data;
}