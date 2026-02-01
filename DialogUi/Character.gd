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

const CHARACTER_DETAILS : Dictionary = {
	Name.Receptionist: {
		"name": "Receptionist", 
		"sprite": preload ("res://Resources/Textures/Characters/receptionist.png"),
		"textSound": "normal"
	}, 
	Name.Player: {
		"name": "Player", 
		"sprite": null,
		"textSound": null
	}, 
	Name.Son: {
		"name": "Son",
		"sprite": preload("res://Resources/Textures/Characters/unnamed.png"),
		"textSound": "normal"
	}
}

static func get_enum_from_string(string_value: String) -> int:
	if Name.has(string_value):
		return Name[string_value]
	else:
		push_error("Invalid Character Name: " + string_value)
		return -1 
