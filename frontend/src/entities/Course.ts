import { Entity, Of, createEntityStore } from 'entity-of';


@Entity
export class Course {
  @Of(() => String, { optional: true })
  id = ''
  @Of(() => String)
  title = ''
  @Of(() => Number)
  semester = 0
  @Of(() => Number)
  credits = 0

  static of = Entity.of<Course>();
}
