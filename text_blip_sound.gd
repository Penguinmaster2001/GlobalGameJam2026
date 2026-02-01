extends AudioStreamPlayer

static var sounds : Dictionary = {
	"normal": load("res://Resources/Audio/normal (1).wav"), }
func play_sound(character_details : Dictionary):
	var character_sus = character_details["textSound"]
	stream = sounds[character_sus]
	play()
