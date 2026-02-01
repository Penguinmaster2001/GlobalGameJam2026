extends Node2D

@export var character_sprite: CharacterSprite
@export var dialog_ui: DialogueUi

var dialog_index : int = 0


var dialog_lines : Array = []
# Called when the node enters the scene tree for the first time.
func _ready():
	#load dialogue
	dialog_lines = load_dialog("res://Resources/Story/story.json")
	#Process the first line of dialogue
	dialog_index = 0
	process_current_line() 
	
func _input(event):
	if event.is_action_pressed("next_line"):
		if dialog_ui.animate_text:
			dialog_ui.skip_text_animation()
		else:
			if dialog_index < len(dialog_lines) - 1:
				dialog_index +=1
				process_current_line()

	
func parse_line(line: String):
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
