import axios from "axios";
import { Student } from "../../entities";

export const useGetStudents = async () => {
  const { data } = await axios.get<Student[]>("/students")
  return await data;
}