MusicDataModule::MusicDataModule(byte id)
{
  _id = id;
  reset();
}
  
void MusicDataModule::update(byte id)
{
  if(id == _id)
  {
    UnityRobot.pop(&_index);
    UnityRobot.pop(&_red);
    UnityRobot.pop(&_green);
    UnityRobot.pop(&_blue);  
  }
}
  
void MusicDataModule::reset()
{
  _index = 0;
  _updated = false;
  _red = 0;
  _green = 0;
  _blue = 0;
}

int MusicDataModule::getIndex()
{
  return (int)_index;
}

int MusicDataModule::getRed()
{
  return (int)_red;
}

int MusicDataModule::getGreen()
{
  return (int)_green;
}

int MusicDataModule::getBlue()
{
  return (int)_blue;
}
