template<typename D, typename B>
class IsDerivedFrom
{
	static void Constraints(D* p)
	{
		B* pb = p; 
		pb = p; 
	}

protected:
	IsDerivedFrom() { void(*p)(D*) = Constraints; }
};

// Force it to fail in the case where B is void
template<typename D>
class IsDerivedFrom<D, void>
{
	IsDerivedFrom() { char* p = (int*)0; }
};