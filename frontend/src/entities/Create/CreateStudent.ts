import { Entity, Of } from 'entity-of';

@Entity
export class CreateStudent {

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

  static of = Entity.of<CreateStudent>();
}
