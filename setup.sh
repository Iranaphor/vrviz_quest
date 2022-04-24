## Setup process for initialisation of oculus quest client for vrviz
# (this process assumes you have installed through cloning /vrviz and completed the setup.sh for that)

# 1. Create a temporary directory 
# (this will be removed at the end)
mkdir temp
cd temp

# 2. download the UnityHub application 
# (you can download this manually from https://unity3d.com/get-unity/download)
wget https://public-cdn.cloud.unity3d.com/hub/prod/UnityHub.AppImage

# 3. Set the application to executable
chmod +x UnityHub.AppImage
./UnityHub.AppImage

# 4. Follow the on screen instructions
#   - accept the user agreement
#   - when prompted for a license, click ignore
#   - move back from preferences using the button in the top right corner

# 5. Lisence management
#   - to add a license click on the settings wheel and click login
#   - enter your personal account details
#   - back on the settings menu go to license management
#   - click on activate new lisence
#   - select Unity Personal, then I dont use Unity in a professional capacity
#   - then click Done

# 6. Complete installation
#   - on the main page, select installs
#   - click Add, and select the Recommended release and click Next
#   - ensure Android Build Support is selected
#   - ensure the options Android SDK & NDK Tools, and Open JDK are selected
#   - click Next and agree to the EULA
#   - this begins the complete installation of Unity

# 7. Add the project


