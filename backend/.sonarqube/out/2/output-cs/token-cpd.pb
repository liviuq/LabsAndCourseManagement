ù	
hD:\Facultate\.net\Proiect\LabsAndCourseManagement\backend\LabsAndCourses.Application\IDatabaseContext.cs
	namespace 	$
LabsAndCoursesManagement
 "
." #
Application# .
{ 
public 

	interface 
IDatabaseContext %
{ 
DbSet 
< 
Student 
> 
Students 
{  !
get" %
;% &
}' (
DbSet		 
<		 
Teacher		 
>		 
Teachers		 
{		  !
get		" %
;		% &
}		' (
DbSet

 
<

 
Course

 
>

 
Courses

 
{

 
get

  #
;

# $
}

% &
DbSet 
< 
Grade 
> 
Grades 
{ 
get !
;! "
}# $
DbSet 
< 
Didactic 
> 
	Didactics !
{" #
get$ '
;' (
}) *
void 
Save 
( 
) 
; 
} 
} 