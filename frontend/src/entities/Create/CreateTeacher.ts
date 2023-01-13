import { Entity, Of, createEntityStore } from 'entity-of';


@Entity
export class CreateTeacher {
  @Of(() => String)
  firstName = ''
  @Of(() => String)
  lastName = ''
  @Of(() => String)
  email = ''
  @Of(() => String)
  teachingDegree = ''

  static of = Entity.of<CreateTeacher>();
}
