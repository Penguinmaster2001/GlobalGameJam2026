class_name Character
extends Node

enum Name {
	Player,
	Receptionist,
	Masked1,
	Masked2,
	Masked3,
	Masked4,
	Son
}

static var CHARACTER_DETAILS : Dictionary = {}

static func get_enum_from_string(string_value: String) -> int:
	if Name.has(string_value):
		return Name[string_value]
	else:
		push_error("Invalid Character Name: " + string_value)
		return -1 
