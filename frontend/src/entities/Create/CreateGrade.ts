import { Entity, Of } from 'entity-of';


@Entity
export class CreateGrade {
  @Of(() => Number)
  value = 0
  @Of(() => String)
  gradeDate = "2023-01-13T11:14:43.292Z"
  @Of(() => Boolean)
  isLabGrade = true
  @Of(() => Boolean)
  isExamGrade = true

  static of = Entity.of<CreateGrade>();
}
