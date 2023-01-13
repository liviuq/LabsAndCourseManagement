import axios from "axios";
import { Course } from "../../entities";

export const useGetCourses = async () => {
  const { data } = await axios.get<Course[]>("/courses")
  return await data;
}
