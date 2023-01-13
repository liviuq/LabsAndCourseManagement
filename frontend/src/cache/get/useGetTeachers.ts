import axios from "axios";
import { Teacher } from "../../entities";

export const useGetTeachers = async () => {
  const { data } = await axios.get<Teacher[]>("/teachers")
  return await data;
}