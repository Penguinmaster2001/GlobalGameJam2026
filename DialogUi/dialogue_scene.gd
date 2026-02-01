extends Node2D

@export var character_sprite: CharacterSprite
@export var dialog_ui: DialogueUi

signal dialogue_finished

var dialog_index : int = 0
var in_dialogue : bool = false


func _ready() -> void:
	hide_children()


var dialog_lines : Array = []
# Called when the node enters the scene tree for the first time.
func start_dialogue(lines: Array, characters: Dictionary, responses: Array):
	Character.CHARACTER_DETAILS = characters
	show_children()
	in_dialogue = true
	#load dialogue
	dialog_lines = lines # load_dialog("res://Resources/Story/story.json")
	#Process the first line of dialogue
	dialog_index = 0
	process_current_line()
	
func _input(event):
	if in_dialogue && event.is_action_pressed("next_line"):
		if dialog_ui.animate_text:
			dialog_ui.skip_text_animation()
		else:
			if dialog_index < len(dialog_lines) - 1:
				dialog_index += 1
				process_current_line()
			else:
				in_dialogue = false
				emit_signal("dialogue_finished")


	
func parse_line(line: String):
	print(line)
	var line_info = line.split(":")
	assert(len(line_info) >= 2)
	return {
		"speaker_name": line_info[0],
		"dialog_line": line_info[1]
	}
	
func process_current_line():
	var line = dialog_lines[dialog_index]
	var line_info = parse_line(line)
	var character_name = Character.get_enum_from_string(line_info["speaker_name"])
	dialog_ui.speaker_name.text = line_info["speaker_name"]
	dialog_ui.change_line(character_name, line_info["dialog_line"])
	character_sprite.change_character(character_name)
	
	
func load_dialog(file_path):
	#open the file
	var file = FileAccess.open(file_path, FileAccess.READ)
	
	#read the content
	var content = file.get_as_text()
	
	#parse the json
	var json_content = JSON.parse_string(content)
	
	#return the dialogue
	return json_content

func hide_children():
	for child in get_children():
		child.visible = false

func show_children():
	for child in get_children():
		child.visible = true
