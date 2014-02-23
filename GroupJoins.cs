//linq pad example
void Main()
{
	var original = new List<Person>() { 
		new Person() { Id = 1, Name = "Bryan" },  
		new Person() { Id = 2, Name = "Randy" }, 
		new Person() { Id = 3, Name = "Jill" } };
	var updated = new List<Person>() { 
		new Person() { Id = 1, Name = "Bryan" },  
		new Person() { Id = 2, Name = "Randi" }, 
		new Person() { Id = 4, Name = "Jack" } };
	
	var findAdds =
	updated.GroupJoin(original, u => u.Id, o => o.Id,
		(u, o) => new { u, o })
	.Where(x => !x.o.Any())
	.Select(x => x.u);
	
	var deletesQuery =
	original.GroupJoin(updated, u => u.Id, o => o.Id,
				(left, right) => new { left, right }
			)
	.Where(j => !j.right.Any())
	.Select(x => x.left);
	
	var updatesQuery =
	original.GroupJoin(updated, u => u.Id, o => o.Id, 
				(left, right) => new { left, right }
			)
	.SelectMany(g =>  g.right.Where(u => u.Name != g.left.Name), 
			(g, rightsingle) => new { g.left, rightsingle })
	.Select(mg => mg.rightsingle);
	
	findAdds.Dump("Addding");
	updatesQuery.Dump("Updating");
	deletesQuery.Dump("Deletes");
}

// A Person
public class Person{

	public int Id { get; set; }
	public string Name { get; set; }

}