class_name DialogueUi extends Control

@export var dialog_line: RichTextLabel
@export var speaker_name: Label
@export var text_blip_sound: AudioStreamPlayer
@export var text_blip_timer: Timer
@export var sentence_pause_timer: Timer

const ANIMATION_SPEED : int = 30
const NO_SOUNDS_CHARS : Array = [",", "!", "?", "."]

var animate_text : bool = false
var current_visible_characters : int = 0
var current_character_details : Dictionary

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	#connect signals
	text_blip_timer.timeout.connect(_on_text_blip_timeout)
	sentence_pause_timer.timeout.connect(_on_sentence_pause_timeout)
	
func _process(delta):
	if animate_text and sentence_pause_timer.is_stopped():
		if dialog_line.visible_ratio < 1:
			dialog_line.visible_ratio += (1.0/dialog_line.text.length()) * (ANIMATION_SPEED * delta)
			if dialog_line.visible_characters > current_visible_characters:
				current_visible_characters = dialog_line.visible_characters
				var current_char = dialog_line.text[current_visible_characters - 1]
				if current_visible_characters < dialog_line.text.length():
					var next_char = dialog_line.text[current_visible_characters]
					if NO_SOUNDS_CHARS.has(current_char) and next_char == " ":
						text_blip_timer.stop()
						sentence_pause_timer.start()
		else:
			animate_text = false
			text_blip_timer.stop()
			
func change_line(character_name: String, line: String):
	speaker_name.text = character_name
	current_visible_characters = 0
	dialog_line.text = line
	dialog_line.visible_characters = 0
	animate_text = true
	text_blip_timer.start()
	
	
func skip_text_animation():
	dialog_line.visible_ratio = 1

func _on_text_blip_timeout():
	text_blip_sound.play_sound("normal")

func _on_sentence_pause_timeout():
	text_blip_timer.start()	
