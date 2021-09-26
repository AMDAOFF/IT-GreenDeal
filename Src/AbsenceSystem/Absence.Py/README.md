For optimal performance with CNN configure dlib to use GPU instead of CPU (If your GPU supports CUDA)

If your GPU does not support CUDA then switch "cnn" out with "hog"

In order to make dlib use your GPU use this guide:

Install
- CUDA
- cuDNN

Go to your solution and use the following commands in CMD:

git clone https://github.com/davisking/dlib.git
cd dlib
mkdir build
cd build
cmake .. -DDLIB_USE_CUDA=1 -DUSE_AVX_INSTRUCTIONS=1

---> Make sure it says that it ENABLED CODA before you continue

cmake --build .
cd ..
python setup.py install
