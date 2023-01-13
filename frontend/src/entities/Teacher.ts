import { Entity, Of } from 'entity-of';

@Entity
export class Teacher {
  @Of(() => String, { optional: true })
  id = ''
  @Of(() => String)
  firstName = ''
  @Of(() => String)
  lastName = ''
  @Of(() => String)
  email = ''
  @Of(() => String)
  teachingDegree = ''

  static of = Entity.of<Teacher>();
}
