import { Entity, Of } from 'entity-of';

@Entity
export class Grade {
  @Of(() => String, { optional: true })
  id = ''
  @Of(() => String, { optional: true })
  studentId = ''
  @Of(() => String, { optional: true })
  courseId = ''
  @Of(() => Number)
  value = 5
  @Of(() => String)
  gradeDate = ''
  @Of(() => Boolean, { optional: true })
  isLabGrade = false
  @Of(() => Boolean, { optional: true })
  isExamGrade = false

  static of = Entity.of<Grade>();
}
