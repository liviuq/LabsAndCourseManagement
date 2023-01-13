import { Entity, Of } from 'entity-of';
import { Grade } from '.';

@Entity
export class Student {
  @Of(() => String, { optional: true })
  id = ''
  @Of(() => String)
  firstName = ''
  @Of(() => String)
  lastName = ''
  @Of(() => String)
  email = ''
  @Of(() => Number)
  semester = 0
  @Of(() => String)
  group = ''
  @Of(() => Number)
  scholarship = 0
  @Of(() => [Grade])
  grades = []
  static of = Entity.of<Student>();
}
